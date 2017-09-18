using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Antlr.Runtime.Misc;
using AuthWebApi.Models;
using AuthWebApi.Services;

namespace AuthWebApi.Controllers
{
    public class IntervalsController : ApiController
    {
        private IntervalsRepository intervalsRepository;

        public IntervalsController()
        {
            intervalsRepository = new IntervalsRepository();
        }

        //[Route("intervals")]
        //[HttpGet]
        //public List<Interwaly> GetIntervals(string token)
        //{
          // return intervalsRepository.GetAllInterwals();
        //}
    }
}
