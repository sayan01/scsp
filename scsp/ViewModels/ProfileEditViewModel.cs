using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using scsp.Models;
namespace scsp.ViewModels
{
    public class ProfileEditViewModel
    {
        public User currentuser {get; set;} = new User();
    }
}