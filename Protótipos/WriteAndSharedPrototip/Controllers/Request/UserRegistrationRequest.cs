using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WriteAndSharedPrototip.Controllers.Request
{
    public class UserRegistrationRequest
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string dataNascimento { get; set; }

        [Required]
        [Compare("Password")]

        public string comparePassowrd { get; set; }
    }
}
