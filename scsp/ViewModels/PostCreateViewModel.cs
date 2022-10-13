using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using scsp.Models;
namespace scsp.ViewModels
{
    public class PostCreateViewModel
    {
        public string AlertMsg {get; set;} = "";
        public string AlertType {get; set;} = "danger";
        public string Content {get; set;} = "";
    }
}