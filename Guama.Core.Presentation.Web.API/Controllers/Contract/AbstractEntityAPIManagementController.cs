using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;

namespace MOROPO.Framework.Guama.Core.Presentation.Web.API.Controllers.Contract
{
    public abstract class AbstractEntityAPIManagementController<TEntity, TId, TInputModel, TDisplayModel> : AbstractEntityAPIConsultController<TEntity, TId, TDisplayModel>
        where TEntity : class, Persistence.Entities.Contract.IEntity<TId>
        where TInputModel : class, Persistence.Presentation.Models.Contract.IEntityInputModel<TEntity, TId>, new()
        where TDisplayModel : class, Persistence.Presentation.Models.Contract.IEntityDisplayModel<TEntity, TId>, new()
    {
        protected readonly ICommandEntityRepository<TEntity, TId> commandRepository;

        public AbstractEntityAPIManagementController(ICommandEntityRepository<TEntity, TId> commandRepository, IQueryEntityRepository<TEntity, TId> queryRepository, IStringLocalizer localizer, ILogger<AbstractEntityAPIManagementController<TEntity, TId, TInputModel, TDisplayModel>> logger) 
            : base(queryRepository, localizer, logger)
        {
            this.commandRepository = commandRepository;
        }

        [HttpPost]
        public virtual ActionResult Create([FromBody] TInputModel model)
        {
            ValidateCreate(model);

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = model.Export();

                    PreCreate(entity, model);

                    commandRepository.Add(entity, User.Identity.Name);

                    PostCreate(entity, model);

                    return Ok();
                }
                catch (System.Exception e)
                {
                    logger.LogError(e, e.Message);

                    return Conflict();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public virtual ActionResult Edit([FromBody] TInputModel model)
        {
            ValidateEdit(model);

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = commandRepository.Get(model.Id);

                    model.Mergue(entity);

                    PreEdit(entity, model);

                    commandRepository.Update(entity, User.Identity.Name);

                    PostEdit(entity, model);

                    return Ok();
                }
                catch (System.InvalidOperationException e)
                {
                    logger.LogError(e, e.Message);

                    return NoContent();
                }
                catch (System.Exception e)
                {
                    logger.LogError(e, e.Message);

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
                var entity = commandRepository.Get(key);

                PreDelete(entity);

                commandRepository.Delete(entity, User.Identity.Name);

                PostDelete(entity);

                return Ok();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e, e.Message);

                return Conflict();
            }
            catch (System.Exception e)
            {
                logger.LogError(e, e.Message);

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
