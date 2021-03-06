﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ResearchProfilerRepo.GRPC;

namespace ResearchProfilerRepo
{
    public class ResearchProfileService : ResearcherService.ResearcherServiceBase
    {
        private readonly ILogger _logger;

        public ResearchProfileService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ResearchProfileService>();
        }

        public override Task<ResearcherListReply> GetAllResearcherInformation(GetAllResearcherRequest request, 
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
