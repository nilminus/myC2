using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamServer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamServer.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Controllers.ClientController.GetConnectedClients();
        }

        [AllowAnonymous]
        [HttpPost]
        public ClientAuthenticationResult ClientLogin([FromBody] ClientAuthenticationRequest request)
        {
            return Controllers.ClientController.ClientLogin(request);
        }
    }
}
