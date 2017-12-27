using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Nest;
using SSO.Demo.Sso.Instrumentation;
using SSO.Demo.Sso.Model.SystemLog;
using SSO.Demo.Toolkits.Extension;
using SSO.Demo.Toolkits.Helper;
using SSO.Demo.Toolkits.Model;

namespace SSO.Demo.Sso.Controllers
{
    public class SystemLogController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List(PageListParam<SystemLogListParam> pageListParam)
        {
            var listParam = pageListParam.Params;

            var uris = new[]
            {
                new Uri("http://192.168.20.81:9200")
            };

            using (var connectionPool = new SniffingConnectionPool(uris))
            {
                using (var settings = new ConnectionSettings(connectionPool).DefaultIndex("chengongtestdb"))
                {
                    settings.DeadTimeout(TimeSpan.FromMinutes(1));
                    settings.MaxRetryTimeout(TimeSpan.FromMinutes(1));
                    settings.ConnectionLimit(100);

                    var client = new ElasticClient(settings);
                    var qwe = client.Search<object>(a => a.AllIndices()
                        .AllTypes().From(pageListParam.Limit * (pageListParam.Page - 1))
                        .Size(pageListParam.Limit).Query(q => q.Match(b => b.Field("_all").Query(listParam.Content))));

                    var documents = qwe.Hits.Select(a => new SystemLogList { Content = a.Source.ToJson(), Index = a.Index, Type = a.Type }).ToList();

                    return PageListResult(new PageListResult(documents, (int)((SearchResponse<object>)qwe).Total));
                }
            }
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(SystemLogFormParams param)
        {
            var client = new MongoClient("mongodb://192.168.20.80:27017");

            var db = client.GetDatabase(param.DataBaseName);
            var coll = db.GetCollection<SystemLog>(param.ColletionName);
            coll.InsertOne(new SystemLog { _id = Guid.NewGuid().ToString(), Content = param.Content });

            return Json(ServiceResult.IsSuccess("添加成功"));
        }
    }

    public class SystemLog
    {
        public string _id;

        [Required]
        public string Content { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateDateTime = DateTime.Now;
    }
}