using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WriteAndSharedPrototip.Controllers.Request
{
    public class UpdateUserRequest
    {
        public string UserName { get; set; }
        public string email { get; set; }
        public string DataNascimento { get; set; }
        public string password { get; set; }
        public string comfirmPassword { get; set; }
    }
}
