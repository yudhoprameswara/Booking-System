using BookingSystem.Provider;
using Microsoft.AspNetCore.Mvc;

namespace Booking_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserProvider userProvider;
        public UserController(UserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var UserIndex = userProvider.GetIndex();
            return Ok(UserIndex);
        }



    }
}
