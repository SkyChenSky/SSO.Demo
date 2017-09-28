namespace SSO.Demo.Toolkits.Model
{
    public class PageListParam<TParam> where TParam : new()
    {
        public PageListParam()
        {
            Params = new TParam();
        }
        public int Limit { get; set; }

        public int Page { get; set; }

        public TParam Params { get; set; }
    }
}
