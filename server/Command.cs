using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    internal class Command
    {
        public const string ProccessList = "PROCLIST";
        public const string Kill = "Kill";
        public const string Run = "RUN";
        public string? Text { get; set; }
        public string? Param { get; set; }
    }
}
