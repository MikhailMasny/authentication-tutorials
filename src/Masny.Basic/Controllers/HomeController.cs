using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Masny.Basic.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        [Authorize(Policy = "Claim.Secret")]
        public IActionResult Policy()
        {
            return View(nameof(Secret));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View(nameof(Secret));
        }

        public IActionResult Authenticate()
        {
            var basicClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString())
            };
            var anotherClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Some Name"),
                new Claim("BasicClaim", "Name@name.name"),
                new Claim(ClaimTypes.NameIdentifier, "Secret"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var basicIdentity = new ClaimsIdentity(basicClaims, "Basic Identity");
            var anotherIdentity = new ClaimsIdentity(anotherClaims, "Another Identity");
            var userPrincipal = new ClaimsPrincipal(new[] { basicIdentity, anotherIdentity });

            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }
    }
}
