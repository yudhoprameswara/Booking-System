using BookingSystem.DataModel.Master.Dropdown;
using BookingSystem.DataModel.Master.Room;
using BookingSystem.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly RoomProvider roomProvider;
        public RoomController(RoomProvider roomProvider)
        {
            this.roomProvider = roomProvider;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var RoomIndex = roomProvider.GetIndex();
            return Ok(RoomIndex);
        }


        [HttpPost]
        public IActionResult CreateEditRoom(CreateEditRoomVM model)
        {
            try
            {
                if (model.Id != 0 && model.Id != null)
                {
                    roomProvider.UpdateRoom(model);
                }
                else
                {
                    roomProvider.InsertRoom(model);

                }
                return Ok(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }


        [HttpDelete]
        public IActionResult DeleteRoom(int id)
        {
            roomProvider?.DeleteRoom(id);
            return Ok();
        }

        [HttpGet("GetSingle")]
        public IActionResult GetSingle(int id)
        {
            var model = roomProvider.GetSingle(id);
            return Ok(model);
        }

        [HttpGet("dropdown")]
        public ActionResult<ListDropdown> GetLocationDropdown()
        {
            var list = roomProvider.LocationDropdown();
            return Ok(list);
        }
    }
}
