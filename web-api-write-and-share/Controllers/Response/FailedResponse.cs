﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_write_and_share.Controllers.Response
{
    public class FailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }

}
