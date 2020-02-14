using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_write_and_share.Controllers
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
            public const string AddFriend = Base + "/friendslist/add";
            public const string RemoveFriend = Base + "/friendslist/remove";
            public const string GetAllFriends = Base + "/friendslist/friends";
        }
    }

}
