using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Orchestrator.Site
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : Controller
    {
        [HttpPost, Route("refresh/orchestrator/luismodels")]
        public RefreshLuisModelsReponse RefreshLuisModels(IEnumerable<ChildBotLuisModelDetails> childBotLuisModelDetails)
        {
            var response = new RefreshLuisModelsReponse();
            var childBotRefreshLuisModelsReponses = new List<ChildBotRefreshLuisModelsReponse>();
            foreach (var luisModel in childBotLuisModelDetails ?? new List<ChildBotLuisModelDetails>())
            {
                try
                {
                    string strCmdText = $"/C bf luis:version:export --appId {luisModel.nlpAppId} --versionId {luisModel.nlpVersion} --endpoint {luisModel.nlpAuthorUrl} --subscriptionKey {luisModel.nlpAuthorKey} --out CognitiveModels/{luisModel.nlpModelName}.json --force";
                    System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                    childBotRefreshLuisModelsReponses.Add(new ChildBotRefreshLuisModelsReponse
                    {
                        botId = luisModel.botId,
                        isRefreshSuccess = true
                    });
                }
                catch
                {
                    childBotRefreshLuisModelsReponses.Add(new ChildBotRefreshLuisModelsReponse
                    {
                        botId = luisModel.botId,
                        isRefreshSuccess = false
                    });
                }
            }

            response.childBotRefreshLuisModelsReponses = childBotRefreshLuisModelsReponses;

            try
            {
                string strCmdText = "/C bf orchestrator:create --hierarchical --in ./CognitiveModels --out ./generated --model ./model --entityModel ./model/entity";
                System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                response.isOrchestratorSnapshotGeneratedSuccessfully = true;
            }
            catch
            {
                response.isOrchestratorSnapshotGeneratedSuccessfully = false;
            }
            return response;
        }
    }

    public class ChildBotLuisModelDetails
    {
        public string botId { get; set; }
        public string nlpAppId { get; set; }
        public string nlpModelName { get; set; }
        public string nlpAuthorKey { get; set; }
        public string nlpAuthorUrl { get; set; }
        public string nlpVersion { get; set; }
    }

    public class RefreshLuisModelsReponse
    {
        public IList<ChildBotRefreshLuisModelsReponse> childBotRefreshLuisModelsReponses { get; set; }
        public bool isAllChildBotModelRefreshedSuccessfully {
            get
            {
               return !childBotRefreshLuisModelsReponses?.Any(x=> x.isRefreshSuccess == false) ?? true;
            }
        }
        public bool isOrchestratorSnapshotGeneratedSuccessfully { get; set; }
    }

    public class ChildBotRefreshLuisModelsReponse
    {
        public string botId { get; set; }
        public bool isRefreshSuccess { get; set; }
    }
}
