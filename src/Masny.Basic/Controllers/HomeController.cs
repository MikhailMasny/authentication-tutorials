using Masny.Basic.CustomPolicyProvider;
using Masny.Basic.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Masny.Basic.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public HomeController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            //var claims = User.Claims.ToList();
            return View(new SecurityViewModel
            {
                Name = "by Default"
            });
        }

        //[Authorize(Policy = "SecurityLevel.5")]
        [Authorize(Policy = "Claim.Secret")]
        public IActionResult Policy()
        {
            return View(nameof(Secret), new SecurityViewModel
            {
                Name = "by Policy"
            });
        }

        [SecurityLevel(5)]
        public IActionResult SecretPolicy()
        {
            return View(nameof(Secret), new SecurityViewModel
            {
                Name = "by Custom Policy - Level 5"
            });
        }

        [SecurityLevel(10)]
        public IActionResult HigherSecretPolicy()
        {
            return View(nameof(Secret), new SecurityViewModel
            {
                Name = "by Custom Policy - Level 10"
            });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View(nameof(Secret), new SecurityViewModel
            {
                Name = "by Role - Admin"
            });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Authenticate()
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
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(DynamicPolicies.SecurityLevel, "7")
            };

            var basicIdentity = new ClaimsIdentity(basicClaims, "Basic Identity");
            var anotherIdentity = new ClaimsIdentity(anotherClaims, "Another Identity");
            var userPrincipal = new ClaimsPrincipal(new[] { basicIdentity, anotherIdentity });

            await HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }

        // Or write: public async Task<IActionResult> DoStaff([FromServices] IAuthorizationService authorizationService)
        public async Task<IActionResult> DoStaff()
        {
            var builder = new AuthorizationPolicyBuilder("Schema");
            var customPolicy = builder.RequireClaim("Hello").Build();
            var authResult = await _authorizationService.AuthorizeAsync(User, customPolicy);

            if (authResult.Succeeded)
            {
                return View();
            }

            return View();
        }
    }
}
