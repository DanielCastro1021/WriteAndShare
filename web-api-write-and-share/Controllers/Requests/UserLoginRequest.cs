﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_write_and_share.Controllers.Requests
{
    public class UserLoginRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }

}
