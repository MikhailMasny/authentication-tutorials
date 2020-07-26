using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masny.Server.Controllers
{
    public class SecretController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return Ok("secret message from server");
        }
    }
}
