using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;

namespace WebApplication1.CognitiveService
{
    public class HRBotCognitveServices : BotCognitiveServiceBase
    {
        private static readonly Uri BaseUrl = new Uri("https://hrbot1.cognitiveservices.azure.com/luis/prediction/v3.0/apps/3f1a099b-71b7-49df-9873-53f0e440698c/slots/production/predict");

        private static readonly string SubscriptionKey = "77224d2ff79c428283d3bb144528a959";
        public HRBotCognitveServices(HttpClient httpClient) : base(httpClient, BaseUrl, SubscriptionKey)
        {
        }

    }
}
