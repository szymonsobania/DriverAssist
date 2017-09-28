using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AuthWebApi.Models;
using AuthWebApi.Services;

namespace AuthWebApi.Controllers
{
    public class UserController : ApiController
    {
        private UserRepository userRepository;

        public UserController()
        {
            userRepository = new UserRepository();
        }

        [Route("user")]
        [HttpPost]
        public UserProfile GetUser(GetUserToken token)
        {
            return userRepository.GetUser(token.token);
        }

        [Route("user")]
        [HttpPut]
        public Response UpdateUser(UserProfile user)
        {
            return userRepository.UpdateUser(user);
        }

        [Route("checkadmin")]
        [HttpPost]
        public Response CheckAdmin(GetUserToken token)
        {
            return userRepository.CheckAdmin(token.token);
        }
    }
}
