using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using scsp.Models;
namespace scsp.ViewModels
{
    public class PostDetailsViewModel
    {
        public string AlertMsg {get; set;} = "";
        public string AlertType {get; set;} = "danger";
        public Post Post {get; set;} = new Post();
        public bool likedbyme {get; set;}
        public bool dislikedbyme {get; set;}
    }
}