using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_write_and_share.Entities
{
    public class Friends
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid FriendOfUserId { get; set; }
    }
}
