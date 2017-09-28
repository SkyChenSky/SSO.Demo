namespace SSO.Demo.Toolkits.Model
{
    public class Query<TParam> where TParam : new()
    {
        public Query()
        {
            Count = 0;
            Params = new TParam();
        }

        public int Count { get; set; }

        public int Code { get; set; }

        public string Msg { get; set; }

        public TParam Params { get; set; }

    }
}
