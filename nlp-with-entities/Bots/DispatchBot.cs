using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Orchestrator;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace Microsoft.BotBuilderSamples
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
            //var allScores = await _orchestractorNLPDispatcher.RecognizeAsync(dc, (Activity)turnContext.Activity, cancellationToken);
            //var topIntent = allScores.Intents.First().Key;

            // Detected entities which could be used to help with intent dispatching, for example, when top intent score is too low or
            // when there are multiple top intents with close scores.
            //var entities = allScores.Entities.Children().Where(t => t.Path != "$instance");
            
            // Next, we call the dispatcher with the top intent.
            //await DispatchToTopIntentAsync(turnContext, topIntent, cancellationToken);
            await DispatchToTopIntentAsync(turnContext, "HR", cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            const string WelcomeText = "Type a greeting, or a question about the weather to get started.";

            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Welcome to Dispatch bot {member.Name}. {WelcomeText}"), cancellationToken);
                }
            }
        }

        // TODO:
        private async Task DispatchToTopIntentAsync(ITurnContext<IMessageActivity> turnContext, string intent, CancellationToken cancellationToken)
        {
            await turnContext.SendActivityAsync(MessageFactory.Text($"Dispatch unrecognized intent: {intent}."), cancellationToken);
            switch (intent)
            {
                case "HRChildBot":
                    //await ProcessHRAutomationAsync(turnContext, cancellationToken);
                    break;
                case "KB":
                    //await ProcessKBAsync(turnContext, cancellationToken);
                    break;
                default:
                    _logger.LogInformation($"Dispatch unrecognized intent: {intent}.");
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Dispatch unrecognized intent: {intent}."), cancellationToken);
                    break;
            }
        }
    }
}
