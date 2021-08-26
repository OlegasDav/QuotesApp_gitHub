using QuotesApp.Contracts;
using QuotesApp.Models.Favqs.RequestModelS;
using QuotesApp.Models.Favqs.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotesApp
{
    interface IFavqsClient
    {
        Task<QuoteResponseModel> GetAllQuotes();

        Task<Quote> GetQuote(int id);

        Task<Quote> AddQuote(QuoteRequestModel quote, string userToken);

        Task<Quote> FavQuote(int id, string userToken);

        Task<UserResponseModel> CreateSession(LoginRequestModel user);

        Task<MessageResponseModel> DestroySession(string userToken);
    }
}
