using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Masny.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretController : ControllerBase
    {
        [Authorize]
        public string Index()
        {
            return "secret message from api";
        }
    }
}
