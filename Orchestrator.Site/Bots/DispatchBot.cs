using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Orchestrator;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace Orchestrator.Site
{
    public class DispatchBot : ActivityHandler
    {
        private ILogger<DispatchBot> _logger;
        private OrchestratorRecognizer _orchestractorNLPDispatcher;

        public DispatchBot(OrchestratorRecognizer orchestractorNLPDispatcher, ILogger<DispatchBot> logger)
        {
            _logger = logger;
            _orchestractorNLPDispatcher = orchestractorNLPDispatcher;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
           var dc = new DialogContext(new DialogSet(), turnContext, new DialogState());
            // Top intent tell us which cognitive service to use.
            var allScores = await _orchestractorNLPDispatcher.RecognizeAsync(dc, (Activity)turnContext.Activity, cancellationToken);
            var topIntent = allScores.Intents.First().Key;
            var topIntentScore = allScores.Intents.First().Value.Score;

            await turnContext.SendActivityAsync(MessageFactory.Text($"{topIntent} : {topIntentScore}"), cancellationToken);
        }
    }
}
