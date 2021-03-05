using System;
using TeamServer.Models;
using Xunit;

namespace xunitTests
{
    public class Client
    {
        [Fact]
        public void SuccessfulClientLogin()
        {
            var request = new ClientAuthenticationRequest { Nick = "nilminus", Password = "a" };
            var result = TeamServer.Controllers.ClientController.ClientLogin(request);

            Assert.Equal(ClientAuthenticationResult.AuthResult.LoginSuccess, result.Result);
            Assert.NotNull(result.Token);
        }

        [Fact]
        public void BadPasswordClientLogin()
        {
            var request = new ClientAuthenticationRequest { Nick = "nilminus", Password = "s" };
            var result = TeamServer.Controllers.ClientController.ClientLogin(request);

            Assert.Equal(ClientAuthenticationResult.AuthResult.BadPassword, result.Result);
            Assert.Null(result.Token);
        }

        [Fact]
        public void NickInUse ()
        {
            var request = new ClientAuthenticationRequest { Nick = "nilminus", Password = "a" };
            TeamServer.Controllers.ClientController.ClientLogin(request);
            var result = TeamServer.Controllers.ClientController.ClientLogin(request);

            Assert.Equal(ClientAuthenticationResult.AuthResult.NickInUse, result.Result);
            Assert.Null(result.Token);
        }

        [Fact]
        public void InvalidRequest()
        {
            var request = new ClientAuthenticationRequest { Nick = "nilminus", Password = "" };
            var result = TeamServer.Controllers.ClientController.ClientLogin(request);

            Assert.Equal(ClientAuthenticationResult.AuthResult.InvalidRequest, result.Result);
            Assert.Null(result.Token);
        }
    }
}
