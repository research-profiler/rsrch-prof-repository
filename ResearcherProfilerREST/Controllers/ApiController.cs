using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResearcherProfilerREST.Database;

namespace ResearcherProfilerREST.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        // GET: Api/5

        [HttpGet("researcher/{mnumber}", Name = "GetResearcher")]
        public JsonResult GetResearcher(string mnumber)
        {
            ApiRespository repo = new ApiRespository();
            var response = repo.GetResearcher(mnumber);
            return new JsonResult(response);
        }

        [HttpGet("aggregation/{aggregationId}/threshold", Name = "GetThresholds")]
        public JsonResult GetThresholds(string aggregationId)
        {
            ApiRespository repo = new ApiRespository();
            var aggId = new Guid(aggregationId);
            var response = repo.GetThresholds(aggId);
            return new JsonResult(response);
        }

        [HttpGet("researcher", Name = "GetResearchers")]
        public JsonResult GetResearchers()
        {
            string searchQuery = Request.Query["search"];
            ApiRespository repo = new ApiRespository();
            var response = repo.GetResearchers(searchQuery);

            return new JsonResult(response);
        }

        [HttpGet("latest", Name = "GetLatest")]
        public JsonResult GetLatest()
        {
            ApiRespository repo = new ApiRespository();
            var response = repo.GetLatest();
            return new JsonResult(response);
        }
    }
}
