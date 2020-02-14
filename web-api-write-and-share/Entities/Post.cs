using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_write_and_share.Entities
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Guid userId { get; set; }
        public long likes { get; set; }
        public string TAGS { get; set; }
        public DateTime Date { get; set; }
    }
}
