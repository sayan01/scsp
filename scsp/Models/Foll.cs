using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace scsp.Models
{
        public class Foll{
        [Key]
        public int ID {get; set;}
        
        [ForeignKey("Follower")]
        public string FollowerId {get; set;} = "";
        [ForeignKey("Followee")]
        public string FolloweeId {get; set;} = "";

        public User Follower { get; set; } = new User();
        public User Followee { get; set; } = new User();
    }
}