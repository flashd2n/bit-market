using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;

namespace Flash.BitMarket.Web.Models.Manage
{
    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }
}