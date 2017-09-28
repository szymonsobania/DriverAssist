using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Antlr.Runtime.Misc;
using AuthWebApi.Models;

namespace AuthWebApi.Services
{
    public class IntervalsRepository
    {
        public List<Interwaly> GetAllInterwals()
        {
            using (PP_testEntities context = new PP_testEntities())
            {
                List<Interwaly> list = new ListStack<Interwaly>();
                foreach (var c in context.Interwalies)
                {
                    list.Add(c);
                }
                return list;
            }
        }

        public Response UpdateInterwalies(List<Interwaly> interwaly)
        {
            using (PP_testEntities context = new PP_testEntities())
            {
                var rows = from o in context.Interwalies
                           select o;
                foreach (var row in rows)
                {
                    context.Interwalies.Remove(row);
                }
                foreach (var i in interwaly)
                {
                    context.Interwalies.Add(i);
                }
                context.SaveChanges();
            }
            return new Response() {Result = "OK"};
        }
    }
}