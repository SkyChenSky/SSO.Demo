using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSO.Demo.Web1.Model.User
{
    public class ListParam
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime? BeganCreateDateTime { get; set; }

        public DateTime? EndCreateDateTime { get; set; }
    }
}
