using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_write_and_share.Controllers.Response
{
    public class PostResponse
    {
        public string Title { get; set; }
        public string Upload { get; set; }
        public string Body { get; set; }
        public string Owner { get; set; }
        public long likes { get; set; }
        public string TAGS { get; set; }
        public DateTime Date { get; set; }
    }
}
