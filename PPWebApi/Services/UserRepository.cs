using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using AuthWebApi.Models;
using Newtonsoft.Json;

namespace AuthWebApi.Services
{
    public class UserRepository
    {
        public UserProfile GetUser(string token)
        {
            if (AuthRepository.IsTokenExist(token))
            {
                string login = AuthRepository.GetLogin(token);
                using (PP_testEntities context = new PP_testEntities())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    context.Configuration.LazyLoadingEnabled = false;
                    var user = (from u in context.Uzytkownicies.Include("Przejazdy_fs").Include("Pojazdies") where u.email == login select u).FirstOrDefault();
                    UserProfile userProfile = new UserProfile();
                    userProfile.administrator = user.administrator;
                    userProfile.email = user.email;
                    userProfile.haslo = user.haslo;
                    userProfile.id_uzytk = user.id_uzytk;
                    userProfile.imie = user.imie;
                    userProfile.nazwisko = user.nazwisko;
                    userProfile.nazwa_uzytk = user.nazwa_uzytk;
                    return userProfile;
                }
            }
            return null;
        }

        public Response UpdateUser(UserProfile user)
        {
            using (PP_testEntities context = new PP_testEntities())
            {
                var userToUpdate = (from u in context.Uzytkownicies.Include("Przejazdy_fs").Include("Pojazdies")
                    where user.email == u.email
                    select u).FirstOrDefault();
                if (userToUpdate != null)
                {
                    userToUpdate.administrator = user.administrator;
                    userToUpdate.email = user.email;
                    userToUpdate.haslo = user.haslo;
                    userToUpdate.id_uzytk = user.id_uzytk;
                    userToUpdate.imie = user.imie;
                    userToUpdate.nazwisko = user.nazwisko;
                    userToUpdate.nazwa_uzytk = user.nazwa_uzytk;
                }
                context.SaveChanges();
            }
            return new Response() {Result = "OK"};
        }
    }
}