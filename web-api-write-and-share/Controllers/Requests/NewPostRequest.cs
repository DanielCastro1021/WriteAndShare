using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_write_and_share.Controllers.Requests
{
    public class NewPostRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public byte[] Upload { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public Guid Owner { get; set; }
        [Required]
        public string TAGS { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
