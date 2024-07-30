using BookingSystem.DataModel.Master;
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
    }
}
