using System.Net.Http;
using System.Threading.Tasks;
using FunctionApp.IntegrationTest.Collections;
using FunctionApp.IntegrationTest.Fixtures;
using Xunit;

namespace FunctionApp.IntegrationTest.Tests
{
    [Collection(nameof(FunctionTestCollection))]
    public class HelloWorldFunctionTest
    {
        private readonly FunctionTestFixture _fixture;
        private HttpResponseMessage _response;

        public HelloWorldFunctionTest(FunctionTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task When_The_HelloWorld_Function_Is_Invoked()
        {
            _response = await _fixture.Client.GetAsync("api/HelloWorld?name=James+Bond");
            Assert.EndsWith("James Bond", await _response.Content.ReadAsStringAsync());
        }
    }
}
