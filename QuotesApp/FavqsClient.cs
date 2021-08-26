using QuotesApp.Contracts;
using QuotesApp.Models.Favqs.RequestModelS;
using QuotesApp.Models.Favqs.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuotesApp
{
    class FavqsClient : IFavqsClient
    {
        private readonly HttpClient _httpClient;

        public FavqsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Quote> AddQuote(QuoteRequestModel quote, string userToken)
        {
            var postJson = JsonSerializer.Serialize(quote);

            var url = $"/api/quotes";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_httpClient.BaseAddress, url),
                Content = new StringContent(postJson, Encoding.UTF8, "application/json")
            };

            request.Headers.Add("User-Token", userToken);

            var response = await _httpClient.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<Quote>();
        }

        public async Task<UserResponseModel> CreateSession(LoginRequestModel user)
        {
            var postJson = JsonSerializer.Serialize(user);

            var url = $"/api/session";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_httpClient.BaseAddress, url),
                Content = new StringContent(postJson, Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<UserResponseModel>();
        }

        public async Task<Quote> FavQuote(int id, string userToken)
        {
            var url = $"/api/quotes/{id}/fav";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(_httpClient.BaseAddress, url)
            };

            request.Headers.Add("User-Token", userToken);

            var response = await _httpClient.SendAsync(request);

            return await response.Content.ReadFromJsonAsync<Quote>();
        }

        public Task<Quote> GetQuote(int id)
        {
            var url = $"/api/quotes/{id}";

            //var response = await _httpClient.GetAsync(url);

            //return await response.Content.ReadFromJsonAsync<Quote>();

            return _httpClient.GetFromJsonAsync<Quote>(url);
        }

        public Task<QuoteResponseModel> GetAllQuotes()
        {
            var url = "/api/quotes";

            return _httpClient.GetFromJsonAsync<QuoteResponseModel>(url);
        }

        public async Task<MessageResponseModel> DestroySession(string userToken)
        {
            var url = $"/api/session";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress, url),
            };

            request.Headers.Add("User-Token", userToken);

            var response = await _httpClient.SendAsync(request);
            return await response.Content.ReadFromJsonAsync<MessageResponseModel>();
        }
    }
}
