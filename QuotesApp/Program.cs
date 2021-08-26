using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace QuotesApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var service = new Startup();
            var serviceProvider = service.ConfigureServices();

            var quotesApp = serviceProvider.GetService<QuatesApp>();

            await quotesApp.StartAsync();
        }
    }
}
