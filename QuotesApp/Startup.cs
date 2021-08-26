using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace QuotesApp
{
    public class Startup
    {
        public IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddHttpClient<IFavqsClient, FavqsClient>(httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://favqs.com");
                httpClient.DefaultRequestHeaders.Add("Authorization", "Token token=b4655f47b530f67f96e39816fade78cf");
                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "247f85ad75f3c69ec121eb84eaa0b747");
            });

            services.AddSingleton<QuatesApp>();

            return services.BuildServiceProvider();
        }
    }
}
