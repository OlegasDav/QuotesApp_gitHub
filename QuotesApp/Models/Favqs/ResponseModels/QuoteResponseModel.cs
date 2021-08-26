using QuotesApp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuotesApp.Models.Favqs.ResponseModels
{
    class QuoteResponseModel
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("last_page")]
        public bool LastPage { get; set; }

        [JsonPropertyName("quotes")]
        public IEnumerable<Quote> Quotes { get; set; }

        public override string ToString()
        {
            return $"Page: {Page}; Last Page: {LastPage} Quote:";
        }
    }
}
