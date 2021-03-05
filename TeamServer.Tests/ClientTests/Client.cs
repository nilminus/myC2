using TeamServer.Models;
using Xunit;

namespace TeamServer.Tests.ClientTests
{
    public class Client
    {
        [Fact]
        public void SuccessfulClientLogin()
        {
            var request = new ClientAuthenticationRequest { Nick = "nilminus", Password = "a" };
            var result = Controllers.ClientController.ClientLogin(request);

            Assert.Equal(ClientAuthenticationResult.AuthResult.LoginSuccess, result.Result);
            Assert.NotNull(result.Token);
        }
        
        [Fact]
        public void LoginSuccess()
        {
            var request = new ClientAuthenticationRequest { Nick = "nilminus", Password = "a" };
            var result = Controllers.ClientController.ClientLogin(request);
            Assert.Equal(ClientAuthenticationResult.AuthResult.LoginSuccess, result.Result);
            Assert.NotNull(result.Token);
        }
    }
}
