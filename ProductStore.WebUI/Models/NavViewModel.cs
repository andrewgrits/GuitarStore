using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductStore.WebUI.Models
{
    public class NavViewModel
    {
        public IEnumerable<string> Categories { get; set; }
        public string CurrentCategory { get; set; }
    }
}