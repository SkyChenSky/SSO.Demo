using System.Collections.Generic;

namespace SSO.Demo.Web1.Model.Home
{
    public class HomeModel
    {
        public List<HomeMenuModel> Menus { get; set; }
    }

    public class HomeMenuModel
    {
        public string MenuName { get; set; }

        public string Url { get; set; }

        public List<HomeMenuModel> Children { get; set; }
    }
}
