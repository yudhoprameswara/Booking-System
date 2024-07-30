using BookingSystem.DataModel.Master;
using BookingSystem.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingCodeController : ControllerBase
    {
        private  readonly BookingCodeProvider BCProvider;
        public BookingCodeController(BookingCodeProvider BCProvider)
        {
            this.BCProvider = BCProvider;
        }

        [HttpPost]
        public IActionResult CreateEditBC(CreateEditBCVM model)
        {
            try
            {
                if (model.Id != 0 && model.Id != null)
                {
                    BCProvider.UpdateBC(model);
                }
                else
                {
                    BCProvider.InsertBC(model);

                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }


        }

        [HttpDelete]
        public IActionResult DeleteBC(int id) { 
            BCProvider?.DeleteBC(id);
            return Ok();
        }


        [HttpGet]
        public IActionResult Index() { 
            var BCIndex =  BCProvider.GetIndex();
            return Ok(BCIndex);
        }

        [HttpGet("GetSingle")]
        public IActionResult GetSingle(int id) { 
            var model = BCProvider.GetSingle(id);
            return Ok(model);
        }
    }
}