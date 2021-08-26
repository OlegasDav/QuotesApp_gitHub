using QuotesApp.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuotesApp.Models.Favqs.RequestModelS
{
    class QuoteRequestModel
    {
        [JsonPropertyName("quote")]
        public QuateShort Quote { get; set; }
    }
}
