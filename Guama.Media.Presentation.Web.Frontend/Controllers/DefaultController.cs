using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Media.Persistence.Entities;

namespace MOROPO.Framework.Guama.Media.Presentation.Web.Frontend.Controllers
{
    [Route("[area]/[controller]")]
    [Area("media")]
    public class DefaultController : Core.Presentation.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IEntityRepository<Thumbnail, System.Guid> thumnailRepository;
        private readonly Common.Managers.Facade.IImageFileManagerFacade imageFileManager;
        private readonly Common.Managers.Facade.IThumbnailFileManagerFacade thumbnailFileManager;

        public DefaultController(IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<Thumbnail, System.Guid> thumnailRepository, Common.Managers.Facade.IThumbnailFileManagerFacade thumbnailFileManager, Common.Managers.Facade.IImageFileManagerFacade imageFileManager, Microsoft.Extensions.Localization.IStringLocalizer<DefaultController> localizer, ILogger<DefaultController> logger)
            : base(localizer, logger)
        {
            this.imageRepository = imageRepository;
            this.thumnailRepository = thumnailRepository;
            this.imageFileManager = imageFileManager;
            this.thumbnailFileManager = thumbnailFileManager;
        }

        [HttpGet]
        [Route("[action]/{key}")]
        public IActionResult Image(System.Guid key)
        {
            try
            {
                var entity = imageRepository.Get(key);

                return new FileContentResult(imageFileManager.GetImageFileContent(entity), entity.ContentType);
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                logger.LogError(e.Message, e.InnerException);
            }
            catch { }

            return new NotFoundResult();
        }

        [HttpGet]
        [Route("[action]/{key}")]
        public IActionResult Thumbnail(System.Guid key)
        {
            try
            {
                var entity = thumnailRepository.Get(key);

                var format = entity.ContentType.Split('/')[1];

                if (format == "jpeg")
                {
                    format = "jpg";
                }

                return new FileContentResult(thumbnailFileManager.GetThumbnailFileContent(entity), entity.ContentType);
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                logger.LogError(e.Message, e.InnerException);
            }
            catch { }

            return new NotFoundResult();
        }
    }
}
