using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using AuthWebApi.Models;
using AuthWebApi.Services;

namespace AuthWebApi.Controllers
{
    public class AuthController : ApiController
    {
        private AuthRepository authRepository;

        public AuthController()
        {
            authRepository = new AuthRepository();
        }

        [Route("auth")]
        [HttpPost]
        public LoginResponse LogIn(LogInUser logInUser)
        {
           return authRepository.LogIn(logInUser);
        } 

        [Route("auth")]
        [HttpPatch]
        public Response LogOut(LogOutUser logOutUser)
        {
            return authRepository.LogOut(logOutUser);
        }

        [Route("auth")]
        [HttpPut]
        public Response Register(RegisterUser registerUser)
        {
            return authRepository.Register(registerUser);
        }
    }
}
