using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using AuthWebApi.Models;
using AuthWebApi.Services;

namespace AuthWebApi.Controllers
{
    public class PassagesController : ApiController
    {
        private PassagesRepository repository;

        public PassagesController()
        {
            repository = new PassagesRepository();
        }

        [Route("passages")]
        [HttpPost]
        public List<Passage> GetPassages(GetUserToken authToken)
        {
            return repository.GetPassages(authToken.token);
        }

        [Route("statistics")]
        [HttpPost]
        public PassageData GetStatistic(GetStatistic pkg)
        {
            return repository.GetStatistic(pkg.token, pkg.passage_guid);
        }

        [Route("updatestat")]
        [HttpPost]
        public PassageData UpdateStatistic(UpdateStatistic stat)
        {
            if (stat.Delete)
                return repository.DeletePassageData(stat);
            return repository.SplitPassageData(stat);
        }
    }
}