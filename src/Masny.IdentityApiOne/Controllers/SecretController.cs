﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Masny.IdentityApiOne.Controllers
{
    public class SecretController : ControllerBase
    {
        [Route("/")]
        public IActionResult Main()
        {
            return Ok("Masny.IdentityApiOne");
        }

        [Route("/secret")]
        [Authorize]
        public IActionResult Secret()
        {
            var claims = User.Claims.ToList();
            return Ok("secret message from ApiOne");
        }
    }
}
