namespace SSO.Demo.Service.Service.Model.MenuService
{
    public class MenuAddAndEditModel
    {
        public string SysMenuId { get; set; }

        public string ParentId { get; set; }

        public string MenuName { get; set; }

        public string Url { get; set; }

        public int Sort { get; set; }

        public int Lv { get; set; }
    }
}
