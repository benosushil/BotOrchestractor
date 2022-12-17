using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication1.CognitiveService
{
    public abstract class BotCognitiveServiceBase
    {
        internal readonly HttpClient _httpclient;
        internal readonly string _subscriptionKey;

        public BotCognitiveServiceBase(HttpClient httpClient,Uri basrUri, string subscriptionKey)
        {
            httpClient.BaseAddress = basrUri;
            _subscriptionKey = subscriptionKey;
            httpClient.Timeout = TimeSpan.FromSeconds(10);
            _httpclient = httpClient;
        }

        public async Task<Model.LuisResponse> FetchIntent(string utterance)
        {
            var queryString = new Dictionary<string, string>();
            queryString["verbose"] = "true";
            queryString["show-all-intents"] = "true";
            queryString["log"] = "true";
            queryString["subscription-key"] = _subscriptionKey;
            queryString["query"] = utterance;
            var repsonse = await _httpclient.GetAsync(Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString("", queryString));
            var jsonString = await repsonse.Content.ReadAsStringAsync();
            var output = JsonConvert.DeserializeObject<Model.LuisResponse>(jsonString);// Newtonsoft.Json.JsonConvert.SerializeObject(result);
            return output;
        }
    }
}
