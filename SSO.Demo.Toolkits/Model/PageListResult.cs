namespace SSO.Demo.Toolkits.Model
{
    public class PageListResult
    {
        public PageListResult()
        {
            
        }
        public PageListResult(object data, int totalCount) 
        {
            Data = data;
            Count = totalCount;
        }
        public object Data { get; set; }

        public int Count { get; set; }

        public int Code { get; set; }

        public string Msg { get; set; }
    }
}
