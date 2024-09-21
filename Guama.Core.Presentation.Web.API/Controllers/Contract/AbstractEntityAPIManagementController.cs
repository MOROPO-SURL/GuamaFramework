using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.API.Controllers.Contract
{
    public abstract class AbstractEntityAPIManagementController<TEntity, TId, TInputModel, TDisplayModel> : AbstractEntityAPIConsultController<TEntity, TId, TDisplayModel>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TInputModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
    {
        public AbstractEntityAPIManagementController(Persistence.Repositories.IEntityRepository<TEntity, TId> repository, IStringLocalizer localizer, ILogger<AbstractEntityAPIManagementController<TEntity, TId, TInputModel, TDisplayModel>> logger) : base(repository, localizer, logger)
        {
        }

        [HttpPost]
        public virtual ActionResult Create([FromBody] TInputModel inputModel)
        {
            ValidateCreate(inputModel);

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = inputModel.Export();

                    PreCreate(entity, inputModel);

                    Repository.Add(entity, User.Identity.Name);

                    PostCreate(entity, inputModel);

                    return Ok();
                }
                catch (System.Exception e)
                {
                    logger.LogError(e.InnerException, e.Message);

                    return Conflict();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public virtual ActionResult Edit([FromBody] TInputModel inputModel)
        {
            ValidateEdit(inputModel);

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = Repository.Get(inputModel.Id);

                    inputModel.Mergue(entity);

                    PreEdit(entity, inputModel);

                    Repository.Update(entity, User.Identity.Name);

                    PostEdit(entity, inputModel);

                    return Ok();
                }
                catch (System.InvalidOperationException e)
                {
                    logger.LogError(e.InnerException, e.Message);

                    return NoContent();
                }
                catch (System.Exception e)
                {
                    logger.LogError(e.InnerException, e.Message);

                    return Conflict();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{key}")]
        public virtual ActionResult Delete(TId key)
        {
            try
            {
                var entity = Repository.Get(key);

                PreDelete(entity);

                Repository.Delete(entity, User.Identity.Name);

                PostDelete(entity);

                return Ok();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                return Conflict();
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);

                return NoContent();
            }
        }

        protected virtual void PreCreate(TEntity entity, TInputModel inputModel) { }

        protected virtual void ValidateCreate(TInputModel inputModel) { }

        protected virtual void PostCreate(TEntity entity, TInputModel inputModel) { }

        protected virtual void PreEdit(TEntity entity, TInputModel inputModel) { }

        protected virtual void ValidateEdit(TInputModel inputModel) { }

        protected virtual void PostEdit(TEntity entity, TInputModel inputModel) { }

        protected virtual void PreDelete(TEntity entity) { }

        protected virtual void PostDelete(TEntity entity) { }
    }
}
