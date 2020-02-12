using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WriteAndSharedPrototip.Routes
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string GetUserById = Base + "/identity/{userId}";
            public const string GetAllUsers = Base + "/identity/users";
            public const string DeleteUser = Base + "/identity/{userId}";
            public const string UpdateUser = Base + "/identity/{userId}";
        }
    }
}
