using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scsp.ViewModels
{
    public class MessageSendAPIViewModel
    {
        public string content {get; set;} = "";
        public string direction {get; set;} = "";
        public string from {get; set;} = ""; 
        public DateTime time {get; set;} = new DateTime();
        
    }
}