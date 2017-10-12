namespace SSO.Demo.Toolkits.Model
{
    public class PageListParam
    {
        public int Limit { get; set; }

        public int Page { get; set; }
    }

    public class PageListParam<TParam> : PageListParam where TParam : new()
    {
        public PageListParam()
        {
            Params = new TParam();
        }

        public TParam Params { get; set; }
    }
}
