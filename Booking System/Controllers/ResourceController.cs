using BookingSystem.DataModel.Master;
using BookingSystem.Provider;
using DanCoffee.Web.Customer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly ResourceProvider resourceProvider;
        private readonly IWebHostEnvironment _env;

        //public ResourceController(ResourceProvider resourceProvider)
        //{
        //    this.resourceProvider = resourceProvider;

        //}

        public ResourceController(ResourceProvider resourceProvider, IWebHostEnvironment env)
        {
            this.resourceProvider = resourceProvider;
            _env = env;
        }


  

        [HttpPost]
        public ActionResult CreateEdit(CreateEditResourceVM model)
        {
            try
            {
                string postFile = "resource";
                if (model.Icon != null && model.IconFile != null)
                {

                    FileHelper.DeleteFile(model.Icon, postFile);
                }
                FileHandler(model, postFile);
                if (model.Id > 0)
                {
                    resourceProvider.UpdateResource(model);
                }
                else
                {
                    resourceProvider.InsertResource(model);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        public IActionResult DeleteResource(int id)
        {
            resourceProvider.DeleteResource(id);
            return Ok();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var ResourceIndex = resourceProvider.GetIndex();
            return Ok(ResourceIndex);
        }

        [HttpGet("GetSingle")]
        public IActionResult GetSingle(int id)
        {
            var model = resourceProvider.GetSingle(id);
            return Ok(model);
        }


        [HttpGet("images/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest("Filename is not provided.");
            }

            var path = Path.Combine(_env.ContentRootPath, "wwwroot/images/resource/", fileName);
            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var image = System.IO.File.OpenRead(path);
            return File(image, contentType);
        }


      
        private void FileHandler(CreateEditResourceVM dto, string url)
        {
            IFormFile multipartFile = dto.IconFile;
            bool isMultipartEmpty = multipartFile == null || multipartFile.Length == 0;
            string icon = (string.IsNullOrEmpty(dto.Icon) && isMultipartEmpty) ? null : dto.Icon;

            try
            {
                if (!isMultipartEmpty)
                {
                    icon = FileHelper.UploadFile(icon, multipartFile, url);
                }

                dto.Icon = icon;
            }
            catch (Exception exception)
            {
            }
        }
    }
}

