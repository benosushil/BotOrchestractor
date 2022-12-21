using ChildBotDetector.Models;
using MongoDB.Driver;
using System;

namespace ChildBotDetector.Repoistory
{
    public class MonogoRepoistory: IRepoistory
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<IChildBot> _mongoCollection;
        public MonogoRepoistory()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://admin123:BotDetector123@cluster0.ph9kn.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            _database = client.GetDatabase("child_bot_detector");
            _mongoCollection = _database.GetCollection<IChildBot>("child_bot_configs");
        }

        public bool AddNewChildBot(IChildBot botDetails)
        {
            try
            {
                _mongoCollection.InsertOne(botDetails);
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public IChildBot FetchChildBotDetails(string botName)
        {
            try
            {
                return _mongoCollection.Find(x => x.Name == botName).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
