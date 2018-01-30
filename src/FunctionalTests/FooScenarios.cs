using Api.Controllers;
using Microsoft.AspNetCore.TestHost;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTests
{
    [Collection("server")]
    public class foo_controller_should
    {
        private readonly HostFixture _fixture;

        public foo_controller_should(HostFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [ResetDatabase]
        public async Task add_new_foo_item()
        {
            var foo = new Foo()
            {
                Bar = "the var value"
            };

            var response = await _fixture.Server
                .CreateHttpApiRequest<FooController>(f => f.PostFoo(foo),new { version = "1" })
                .WithIdentity(new Claim[]
                {
                    new Claim("CustomClaim","the value")
                })
                .PostAsync();

            response.EnsureSuccessStatusCode();
        }
    }
}
