using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace ResearchProfilerRepo
{
    public class ResearchProfileService : ResearcherService.ResearcherServiceBase
    {
        private readonly ILogger _logger;

        public ResearchProfileService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ResearchProfileService>();
        }

        public override Task<ResearcherListReply> GetAllResearchers(GetAllResearcherRequest request, 
            ServerCallContext context)
        {
            return null;
        }

        public override Task<ResearcherReply> GetResearcherById(ResearcherByIdRequest request,
            ServerCallContext context)
        {
            return null;
        }
    }
}
