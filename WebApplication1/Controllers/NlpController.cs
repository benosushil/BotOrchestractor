using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("nlp")]
    [Microsoft.AspNetCore.Cors.EnableCors]
    public class NlpController : ControllerBase
    {

        private readonly IBotFrameworkHttpAdapter _adapter;
        private readonly IBot _bot;
        private readonly ILogger<NlpController> _logger;
        private readonly CognitiveService.HRBotCognitveServices _hrBotService;

        public NlpController(ILogger<NlpController> logger, CognitiveService.HRBotCognitveServices hrBotService)
        {
            _logger = logger;
            _hrBotService = hrBotService;
        }

        [HttpGet]
        public async Task<Model.LuisResponse> Get()
        {
            var result = await _hrBotService.FetchIntent("Who is my manager");
            //var response = _adapter.
            return result;
            //return "Orchestrator Service";
        }

        //[HttpGet, Route()]
        //public 

        [HttpPut, Route("GetBotInfo") ]
        public ChildBots GetBotInfo([FromBody]string utterance)
        {

           // var result = await _hrBotService.FetchIntent("Who is my manager");
            //var response = _adapter.
           // return result;
            Array values = Enum.GetValues(typeof(ChildBots));
            Random random = new Random();
            return (ChildBots)values.GetValue(random.Next(values.Length));
        }

        [HttpPut, Route("Refresh")]
        public void Refresh()
        {
            return;
        }
    }
}
