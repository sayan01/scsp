using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using scsp.Models;
namespace scsp.ViewModels;
public class AuthRegisterViewModel
{
    public User User {get; set;} = new User();
    public string password {get; set;} = "";
    public string Title {get; set;} = "";
    public string AlertMsg {get; set;} = "";
    public string AlertType {get; set;} = "danger";
}
