using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masny.IdentityApiOne.Controllers
{
    public class SecretController : ControllerBase
    {
        [Route("/secret")]
        [Authorize]
        public IActionResult Secret()
        {
            return Ok("secret message from ApiOne");
        }
    }
}
