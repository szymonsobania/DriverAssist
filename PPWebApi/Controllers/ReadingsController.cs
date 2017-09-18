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
    public class ReadingsController : ApiController
    {
        private ReadingsRepository readingsRepository;

        public ReadingsController()
        {
            readingsRepository = new ReadingsRepository();
        }
        [Route("readings")]
        [HttpPost]
        public Response Read(Reading reading)
        {
            return readingsRepository.Read(reading);
        }
    }
}
