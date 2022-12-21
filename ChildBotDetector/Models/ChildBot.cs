using System;
using System.Collections.Generic;
using System.Text;

namespace ChildBotDetector.Models
{
    public class ChildBot : IChildBot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AppId { get; set; }
    }
}
