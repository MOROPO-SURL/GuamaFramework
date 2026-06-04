using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Constants;
using MOROPO.Framework.Guama.Core.Security.Common.Persistence.Entities;
using MOROPO.Framework.Guama.Core.Security.Common.Persistence.Extensions;
using MOROPO.Framework.Guama.Security.Personal.Persistence.Entities;
using MOROPO.Framework.Guama.Security.Personal.Presentation.Web.Backend.Models.Filter;
using MOROPO.Framework.Guama.Security.Personal.Presentation.Web.Backend.Models.View;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;

namespace MOROPO.Framework.Guama.Security.Personal.Presentation.Web.Backend.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("security")]
    public class UsersController : Core.Presentation.Web.Backend.Controllers.Contract.AbstractEnableableEntityManagementFilteredController<User, string, UserInputViewModel, UserDisplayViewModel, UsersFilterModel>
    {
        private readonly IQueryEntityRepository<Role, System.Guid> roleRepository;
        private readonly ICommandEntityRepository<Identity.Persistence.Entities.PersonIdentity, System.Guid> identityRepository;
        private readonly ICommandEntityRepository<Media.Persistence.Entities.Image, System.Guid> imageRepository;
        private readonly Media.Presentation.Web.Common.Managers.Facade.IImageFileManagerFacade imageFileManager;

        public UsersController(Media.Presentation.Web.Common.Managers.Facade.IImageFileManagerFacade imageFileManager, ICommandEntityRepository<Media.Persistence.Entities.Image, System.Guid> imageRepository, ICommandEntityRepository<Identity.Persistence.Entities.PersonIdentity, System.Guid> identityRepository, IQueryEntityRepository<Role, System.Guid> roleRepository, ICommandEntityRepository<User, string> commandRepository, IQueryEntityRepository<User, string> queryRepository, Microsoft.Extensions.Localization.IStringLocalizer<UsersController> localizer, ILogger<UsersController> logger)
            : base(commandRepository, queryRepository, localizer, logger)
        {
            this.roleRepository = roleRepository;
            this.identityRepository = identityRepository;
            this.imageRepository = imageRepository;
            this.imageFileManager = imageFileManager;
        }

        protected override string GetFilterSessionID()
        {
            return "users-management-filter";
        }

        [RequirePermissions(Core.Security.Presentation.Constants.Permissions.UserViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Core.Security.Presentation.Constants.Permissions.UserViewer)]
        public override IActionResult Filter(UsersFilterModel filterModel)
        {
            return base.Filter(filterModel);
        }

        [RequirePermissions(Core.Security.Presentation.Constants.Permissions.UserCreator)]
        public override IActionResult Create()
        {
            var model = new UserWithPasswordInputViewModel();

            ConfigureCreate(model);

            return View(model);
        }

        protected override void PreCreate(User entity, UserInputViewModel model)
        {
            entity.Password = entity.Password.ToHashedPassword();

            entity.Signature = Persistence.ORM.EF.Helpers.Integrity.UserIntegrityHelper.GetSignature(entity);

            if (model.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                model.ImageFile.CopyTo(memoryStream);

                var image = new Media.Persistence.Entities.Image { ContentType = model.ImageFile.ContentType, Length = model.ImageFile.Length, About = "User " + entity.Id };

                imageRepository.Add(image, User.Identity.Name, false);

                imageFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray(), model.ImageFile.ContentType);

                entity.Identity.ImageId = image.Id;
                entity.Identity.Image = image;
            }

            base.PreCreate(entity, model);
        }

        [RequirePermissions(Core.Security.Presentation.Constants.Permissions.UserCreator)]
        public override IActionResult Create(UserInputViewModel model)
        {
            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    ModelState.ClearValidationState("ImageFile");

                    model.ImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(model);

                    updateModelTask.Wait();
                }
            }

            var notification = new Core.Presentation.Models.View.NotificationViewModel
            {
                Type = Core.Presentation.Models.View.NotificationType.Danger,
                Message = GetInvalidMessage()
            };

            Request.Form.TryGetValue("Password", out StringValues passwords);
            Request.Form.TryGetValue("ConfirmPassword", out StringValues confirmations);

            model = new UserWithPasswordInputViewModel
            {
                Password = passwords.Count > 0 ? passwords.ToArray()[0] : string.Empty,
                ConfirmPassword = confirmations.Count > 0 ? confirmations.ToArray()[0] : string.Empty,
                Enabled = model.Enabled,
                HasFullAccess = model.HasFullAccess,
                SelectableRoles = model.SelectableRoles,
                Id = model.Id,
                Name = model.Name,
                LastName = model.LastName,
                ImageFile = model.ImageFile
            };

            if (TryValidateModel(model))
            {
                var entity = model.Export();

                PreCreate(entity, model);

                try
                {
                    commandRepository.Add(entity, User.Identity.Name);

                    PostCreate(entity, model);

                    notification.Type = Core.Presentation.Models.View.NotificationType.Success;
                    notification.Message = GetCreationSuccessMessage();

                    TempData[Core.Presentation.Models.View.NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                    return GetSuccessCreateActionResult(entity.Id);
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
                {
                    logger.LogError(e.Message, e.InnerException);

                    ModelState.AddModelError("Id", "UserNameTakedMessage");
                }
            }

            TempData[Core.Presentation.Models.View.NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return View(model);
        }

        protected override void ConfigureCreate(UserInputViewModel model)
        {
            PopulateModelInputForAvailableRoles(model);
        }

        protected override void PostCreate(User entity, UserInputViewModel model)
        {
            base.PostCreate(entity, model);

            var roles = new Collection<Role>();

            if (model.SelectableRoles != null)
            {
                foreach (var item in model.SelectableRoles.Where(item => item.Selected))
                {
                    roles.Add(roleRepository.Get(item.Id));
                }
            }

            entity.RoleBridges.Clear();

            foreach (var role in roles)
            {
                var bridge = new UserRoleBridge { ProwId = entity.Id, SternId = role.Id };

                bridge.Signature = Persistence.ORM.EF.Helpers.Integrity.UserRoleBridgeIntegrityHelper.GetSignature(bridge);

                entity.RoleBridges.Add(bridge);
            }

            commandRepository.Update(entity, User.Identity.Name);
        }

        [RequirePermissions(Core.Security.Presentation.Constants.Permissions.UserEditor)]
        public override IActionResult Edit(string key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Core.Security.Presentation.Constants.Permissions.UserEditor)]
        public override IActionResult Edit(UserInputViewModel model)
        {
            try
            {
                var entity = queryRepository.Get(model.Id);

                if (entity.Id.Equals(User.Identity.Name) && entity.Enabled != model.Enabled)
                {
                    ModelState.AddModelError("Enabled", GetCurrentUserNotDisactivableMessage());
                }

                if (entity.HasFullAccess && !User.HasClaim(claim => claim.Type.Equals(ClaimTypes.AccountWithFullAccess) && claim.Value.Equals(true.ToString())) && entity.Enabled != model.Enabled)
                {
                    ModelState.AddModelError("Enabled", GetFullAccessUserNotDisactivableMessage());
                }

                if (entity.HasFullAccess && !User.HasClaim(claim => claim.Type.Equals(ClaimTypes.AccountWithFullAccess) && claim.Value.Equals(true.ToString())) && entity.HasFullAccess != model.HasFullAccess)
                {
                    ModelState.AddModelError("HasFullAccess", "FullAccessSessionRequired");
                }
            }
            catch
            {
            }

            ModelState.ClearValidationState("ImageFile");
            ModelState.MarkFieldValid("ImageFile");

            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    model.ImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(model);

                    updateModelTask.Wait();
                }
            }

            return base.Edit(model);
        }

        protected override void ConfigureEdit(User entity, UserInputViewModel model)
        {
            PopulateModelInputForAvailableRoles(model);

            var groups = entity.RoleBridges.Select(item => item.Stern).ToArray();

            foreach (var item in model.SelectableRoles)
            {
                item.Selected = groups.Any(group => group.Id.Equals(item.Id));
            }
        }

        protected override void PreEdit(User entity, UserInputViewModel model)
        {
            base.PreEdit(entity, model);

            if (model.ImageFile == null)
            {
                if (model.ResetImage && entity.Identity.Image != null)
                {
                    imageFileManager.DeleteImageFile(entity.Identity.Image);

                    imageRepository.Delete(entity.Identity.Image, User.Identity.Name);
                }

                return;
            }

            if (entity.Identity.Image != null)
            {
                imageFileManager.DeleteImageFile(entity.Identity.Image);

                imageRepository.Delete(entity.Identity.Image, User.Identity.Name);
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                model.ImageFile.CopyTo(memoryStream);

                var image = new Media.Persistence.Entities.Image { ContentType = model.ImageFile.ContentType, Length = model.ImageFile.Length, About = "User " + entity.Id };

                imageRepository.Add(image, User.Identity.Name, false);

                imageFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray(), model.ImageFile.ContentType);

                entity.Identity.ImageId = image.Id;
                entity.Identity.Image = image;
            }
        }

        protected override void PostEdit(User entity, UserInputViewModel model)
        {
            base.PostEdit(entity, model);

            entity.RoleBridges.Clear();

            commandRepository.Update(entity, User.Identity.Name);

            var roles = new Collection<Role>();

            if (model.SelectableRoles != null)
            {
                foreach (var item in model.SelectableRoles.Where(item => item.Selected))
                {
                    roles.Add(roleRepository.Get(item.Id));
                }
            }

            foreach (var role in roles)
            {
                var bridge = new UserRoleBridge { ProwId = entity.Id, SternId = role.Id };

                bridge.Signature = Persistence.ORM.EF.Helpers.Integrity.UserRoleBridgeIntegrityHelper.GetSignature(bridge);

                entity.RoleBridges.Add(bridge);
            }

            commandRepository.Update(entity, User.Identity.Name);
        }

        [RequirePermissions(Core.Security.Presentation.Constants.Permissions.UserRemover)]
        public override IActionResult Delete(string key)
        {
            var notification = new Core.Presentation.Models.View.NotificationViewModel
            {
                Type = Core.Presentation.Models.View.NotificationType.Danger,
                Message = GetNotFoundMessage()
            };

            try
            {
                var entity = queryRepository.Get(key);

                if (entity.Id.Equals(User.Identity.Name))
                {
                    notification.Type = Core.Presentation.Models.View.NotificationType.Warning;
                    notification.Message = GetCurrentUserNotRemovableMessage();

                    TempData[Core.Presentation.Models.View.NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                    return GetDefaultActionResult();
                }
                else if (entity.HasFullAccess && !User.HasClaim(claim => claim.Type.Equals(ClaimTypes.AccountWithFullAccess) && claim.Value.Equals(true.ToString())))
                {
                    notification.Type = Core.Presentation.Models.View.NotificationType.Warning;
                    notification.Message = GetFullAccessUserNotRemovableMessage();

                    TempData[Core.Presentation.Models.View.NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                    return GetDefaultActionResult();
                }

                return base.Delete(key);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.Message, e.InnerException);
            }

            TempData[Core.Presentation.Models.View.NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

            return GetDefaultActionResult();
        }

        [RequirePermissions(Core.Security.Presentation.Constants.Permissions.UserRemover)]
        public override IActionResult Delete(UserDisplayViewModel model)
        {
            try
            {
                var entity = queryRepository.Get(model.Id);

                if (entity.Id.Equals(User.Identity.Name))
                {
                    var notification = new Core.Presentation.Models.View.NotificationViewModel
                    {
                        Type = Core.Presentation.Models.View.NotificationType.Warning,
                        Message = GetCurrentUserNotRemovableMessage()
                    };

                    TempData[Core.Presentation.Models.View.NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                    return GetDefaultActionResult();
                }
                else if (entity.HasFullAccess && !User.HasClaim(claim => claim.Type.Equals(ClaimTypes.AccountWithFullAccess) && claim.Value.Equals(true.ToString())))
                {
                    var notification = new Core.Presentation.Models.View.NotificationViewModel
                    {
                        Type = Core.Presentation.Models.View.NotificationType.Warning,
                        Message = GetFullAccessUserNotRemovableMessage()
                    };

                    TempData[Core.Presentation.Models.View.NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                    return GetDefaultActionResult();
                }
            }
            catch
            {
            }

            return base.Delete(model);
        }

        protected override void PostDelete(User entity)
        {
            base.PostDelete(entity);

            identityRepository.Delete(entity.Identity, User.Identity.Name);

            if (entity.Identity.Image == null) return;

            imageRepository.Delete(entity.Identity.Image, User.Identity.Name);

            imageFileManager.DeleteImageFile(entity.Identity.Image);
        }

        [RequirePermissions(Core.Security.Presentation.Constants.Permissions.UserEditor)]
        public override IActionResult Enable(string key)
        {
            try
            {
                var entity = queryRepository.Get(key);

                if (entity.Id.Equals(User.Identity.Name))
                {
                    var notification = new Core.Presentation.Models.View.NotificationViewModel
                    {
                        Type = Core.Presentation.Models.View.NotificationType.Warning,
                        Message = GetCurrentUserNotDisactivableMessage()
                    };

                    TempData[Core.Presentation.Models.View.NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                    return GetDefaultActionResult();
                }
                else if (entity.HasFullAccess && !User.HasClaim(claim => claim.Type.Equals(ClaimTypes.AccountWithFullAccess) && claim.Value.Equals(true.ToString())))
                {
                    var notification = new Core.Presentation.Models.View.NotificationViewModel
                    {
                        Type = Core.Presentation.Models.View.NotificationType.Warning,
                        Message = GetFullAccessUserNotDisactivableMessage()
                    };

                    TempData[Core.Presentation.Models.View.NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                    return GetDefaultActionResult();
                }
            }
            catch
            {
            }

            try
            {
                var entity = commandRepository.Get(key);

                entity.Enabled = true;

                entity.Signature = Persistence.ORM.EF.Helpers.Integrity.UserIntegrityHelper.GetSignature(entity);

                commandRepository.Update(entity, User.Identity.Name);

                SetDefaultNotification(Core.Presentation.Models.View.NotificationType.Success, GetEnableSuccessMessage());
            }
            catch (System.Exception e)
            {
                logger.LogError(1, e.InnerException, e.Message, null);

                SetDefaultNotification(Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());
            }

            return GetDefaultActionResult();
        }

        [RequirePermissions(Core.Security.Presentation.Constants.Permissions.UserEditor)]
        public override IActionResult Disable(string key)
        {
            try
            {
                var entity = queryRepository.Get(key);

                if (entity.Id.Equals(User.Identity.Name))
                {
                    var notification = new Core.Presentation.Models.View.NotificationViewModel
                    {
                        Type = Core.Presentation.Models.View.NotificationType.Warning,
                        Message = GetCurrentUserNotDisactivableMessage()
                    };

                    TempData[Core.Presentation.Models.View.NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                    return GetDefaultActionResult();
                }
                else if (entity.HasFullAccess && !User.HasClaim(claim => claim.Type.Equals(ClaimTypes.AccountWithFullAccess) && claim.Value.Equals(true.ToString())))
                {
                    var notification = new Core.Presentation.Models.View.NotificationViewModel
                    {
                        Type = Core.Presentation.Models.View.NotificationType.Warning,
                        Message = GetFullAccessUserNotDisactivableMessage()
                    };

                    TempData[Core.Presentation.Models.View.NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = JsonConvert.SerializeObject(notification);

                    return GetDefaultActionResult();
                }
            }
            catch
            {
            }

            try
            {
                var entity = commandRepository.Get(key);

                entity.Enabled = false;

                entity.Signature = Persistence.ORM.EF.Helpers.Integrity.UserIntegrityHelper.GetSignature(entity);

                commandRepository.Update(entity, User.Identity.Name);

                SetDefaultNotification(Core.Presentation.Models.View.NotificationType.Success, GetEnableSuccessMessage());
            }
            catch (System.Exception e)
            {
                logger.LogError(1, e.InnerException, e.Message, null);

                SetDefaultNotification(Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());
            }

            return GetDefaultActionResult();
        }

        [RequirePermissions(Core.Security.Presentation.Constants.Permissions.UserEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(UserChangePasswordViewModel model)
        {
            SetDefaultNotification(Core.Presentation.Models.View.NotificationType.Danger, "Los datos especificados no son correctos.");

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.UserId == User.Identity.Name)
                    {
                        SetDefaultNotification(Core.Presentation.Models.View.NotificationType.Warning, "Los datos especificados no son correctos.");
                    }
                    else
                    {
                        var user = commandRepository.Get(model.UserId);

                        user.Password = model.NewPassword.ToHashedPassword();
                        user.PasswordChangeRequired = model.ChangePasswordRequired;

                        user.Signature = Persistence.ORM.EF.Helpers.Integrity.UserIntegrityHelper.GetSignature(user);

                        commandRepository.Update(user, User.Identity.Name);

                        SetNextNotification(Core.Presentation.Models.View.NotificationType.Warning, "Cambiar Contraseña.", "Contraseña modificada con éxito.");
                    }
                }
                catch
                {
                    SetDefaultNotification(Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());
                }
            }
            else
            {
                var validationErrorMessages = new System.Collections.Generic.List<string>();

                foreach (var record in ModelState)
                {
                    foreach (var error in record.Value.Errors)
                    {
                        validationErrorMessages.Add(error.ErrorMessage);
                    }
                }

                SetDefaultNotification(Core.Presentation.Models.View.NotificationType.Danger, "Cambiar Contraseña." + (validationErrorMessages.Count > 1 ? " Múltiples errores." : ""), validationErrorMessages.ToArray());
            }

            return RedirectToAction("index");
        }

        public virtual string GetCurrentUserPaswordNotEditableMessage()
        {
            return "CurrentUserPaswordNotEditableMessage";
        }

        protected virtual string GetCurrentUserNotRemovableMessage()
        {
            return "CurrentUserNotRemovableMessage";
        }

        protected virtual string GetCurrentUserNotDisactivableMessage()
        {
            return "CurrentUserNotDisactivableMessage";
        }

        protected virtual string GetFullAccessUserNotDisactivableMessage()
        {
            return "FullAccessUserNotDisactivableMessage";
        }

        protected virtual string GetFullAccessUserNotRemovableMessage()
        {
            return "FullAccessUserNotRemovableMessage";
        }

        private void PopulateModelInputForAvailableRoles(UserInputViewModel modelInput)
        {
            var availableRoles = roleRepository.GetAll().ToList();

            modelInput.SelectableRoles = availableRoles.Select(entity => new Core.Presentation.Models.View.SelectionDescriptedItemInputViewModel<System.Guid>
            {
                Id = entity.Id,
                Selected = false,
                Label = entity.Name,
                Description = entity.Description
            }).ToList();
        }
    }
}
