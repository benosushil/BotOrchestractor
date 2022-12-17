using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class LuisResponse 
    {
        public string query { get; set; }
        public Prediction prediction { get; set; }
    }

    public class Prediction
    {
        public string topIntent { get; set; }
        public Intents intents { get; set; }
        public Entities entities { get; set; }
    }

    public class IntentScore
    {
        public double score { get; set; }
    }

    public class Intents
    {
        [JsonProperty("HRBot.getHRManager")]
        public IntentScore HRBotgetHRManager { get; set; }

        [JsonProperty("HRBot.getNoticePeriodDuration")]
        public IntentScore HRBotgetNoticePeriodDuration { get; set; }
        public None None { get; set; }

        [JsonProperty("HRBot.getAvailableOpening")]
        public IntentScore HRBotgetAvailableOpening { get; set; }
    }

    public class None
    {
        public double? score { get; set; }
    }

    public class Entities
    {
    }
}
