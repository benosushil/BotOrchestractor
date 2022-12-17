using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("FailedPhrase")]
    public class FailedPhraseController : Controller
    {
        [HttpGet]
        public IEnumerable<FailedPhrase> Get()
        {
            //var result = await _hrBotService.FetchIntent("Who is my manager");
            //var response = _adapter.
            //return result;

            return new List<FailedPhrase> 
            {
                new FailedPhrase { Id = 1, Phrase="What is today weather", DetectedIntent = "Game", ConfidenceScore = 0.5M, SuggestedIntent = "weather", SuggestedConfidenceScore = 0.8M, ActualIntent = "" },
                new FailedPhrase { Id = 2, Phrase="How are you doing today", DetectedIntent = "Food", ConfidenceScore = 0.1M, SuggestedIntent = "health", SuggestedConfidenceScore = 0.7M, ActualIntent = "" },
                new FailedPhrase { Id = 3, Phrase="My salary", DetectedIntent = "Promotion", ConfidenceScore = 0.4M, SuggestedIntent = "salary", SuggestedConfidenceScore = 0.4M, ActualIntent = "" }
            };
        }
    }

    public class FailedPhrase

    {

        public int Id { get; set; }
        public string Phrase { get; set; }
        public string DetectedIntent { get; set; }
        public decimal ConfidenceScore { get; set; }
        public string SuggestedIntent { get; set; }
        public decimal SuggestedConfidenceScore { get; set; }
        public string ActualIntent { get; set; }
    }
}
