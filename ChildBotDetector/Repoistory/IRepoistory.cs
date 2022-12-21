using ChildBotDetector.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChildBotDetector.Repoistory
{
    public interface IRepoistory
    {
        bool AddNewChildBot(IChildBot botDetails);

        IChildBot FetchChildBotDetails(string botName);
    }
}
