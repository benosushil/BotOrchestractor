using ChildBotDetector.Models;
using ChildBotDetector.Repoistory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace Orchestrator.Site
{
    [Route("api/childbotadmin")]
    [ApiController]
    public class ChildBotAdminController : Controller
    {
        private readonly IRepoistory _repoistory;
        public ChildBotAdminController(IRepoistory repoistory)
        {
            _repoistory = repoistory;
        }

        [HttpGet]
        public IEnumerable<IChildBot> Index()
        {
            return new List<ChildBot>();
        }

        [HttpPost]
        public bool Create(ChildBot botDetails)
        {
            return _repoistory.AddNewChildBot(botDetails);
        }
    }
}
