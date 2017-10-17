using System;
using System.Collections.Generic;
using System.Text;

namespace SSO.Demo.Service.Service.Model.MenuService
{
    public class ScrollMenuModel
    {
        public string MenuName { get; set; }

        public string Url { get; set; }

        public List<ScrollMenuModel> Children { get; set; }
    }
}
