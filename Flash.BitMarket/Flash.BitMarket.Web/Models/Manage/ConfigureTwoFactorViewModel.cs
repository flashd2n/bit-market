using System.Collections.Generic;
using System.Web.Mvc;

namespace Flash.BitMarket.Web.Models.Manage
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<SelectListItem> Providers { get; set; }
    }
}