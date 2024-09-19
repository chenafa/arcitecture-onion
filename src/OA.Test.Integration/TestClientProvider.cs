using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace OA.Test.Integration
{
    public class TestClientProvider
    {
        public HttpClient Client { get; private set; }

        public TestClientProvider()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Program>());

            Client = server.CreateClient();
        }
    }
}
