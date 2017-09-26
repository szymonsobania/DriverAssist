using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using AuthWebApi.Models;

namespace AuthWebApi.Services
{
    public class AuthRepository
    {
        private static Dictionary<string, string> sessions = new Dictionary<string, string>();

        #region httpmethod

        private static string ByteArrayToHexString(byte[] Bytes)
        {
            StringBuilder Result = new StringBuilder(Bytes.Length * 2);
            string HexAlphabet = "0123456789ABCDEF";

            foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }

            return Result.ToString();
        }

        public static string GetHash(string pass)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(pass);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            return ByteArrayToHexString(hash);
        }

        public LoginResponse LogIn(LogInUser logInUser)
        {
            string haslo = GetHash(logInUser.Password);
            Uzytkownicy user;
            using(PP_testEntities context = new PP_testEntities())
            {
                user = (from u in context.Uzytkownicies
                    where u.email == logInUser.Email && u.haslo == haslo
                        select u).FirstOrDefault();
            }
            if (user == null)
                return new LoginResponse() { Result = "Failed", Reason = "Bad email or password"};

            var time = DateTime.Now;
            string str = time.ToString("yyyyMMddHHmmssfffffff") + logInUser.Email + logInUser.UserAgent;
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            var token = sBuilder.ToString();
            sessions.Add(token,logInUser.Email);
            return new LoginResponse() { Token = token, Result = "OK"};
        }

        public Response LogOut(LogOutUser logOutUser)
        {
            sessions.Remove(logOutUser.AuthToken);
            return new Response() { Result = "OK" };
        }

        public Response Register(RegisterUser registerUser)
        {
            using (PP_testEntities context = new PP_testEntities())
            {
                bool isExist = context.Uzytkownicies.Any(u => u.email == registerUser.Email);
                if (isExist)
                    return new Response() { Result = "Failed", Reason = "User is already exist"};

                var user = new Uzytkownicy()
                {
                    email = registerUser.Email,
                    haslo = registerUser.Password,
                    nazwa_uzytk = registerUser.Name
                };
                context.Uzytkownicies.Add(user);
                context.SaveChanges();
            }
            return new Response() { Result = "OK" };
        }
#endregion

#region Session

        public static bool IsTokenExist(string token)
        {
            return sessions.ContainsKey(token);
        }

        public static string GetLogin(string token)
        {
            if (sessions.ContainsKey(token))
                return sessions[token];
            return string.Empty;
        }
        

#endregion
    }
}