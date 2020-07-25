using Masny.Basic.CookieAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Masny.Basic.Controllers
{
    public class OperationController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public OperationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Open()
        {
            var cookieJar = new CookieJar();
            await _authorizationService.AuthorizeAsync(User, cookieJar, CookieJarAuthOperations.Open);
            return View();
        }
    }
}
