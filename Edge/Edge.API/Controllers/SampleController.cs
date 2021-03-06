﻿using Edge.Models.DatabaseModels;
using Edge.Services.Interface;
using Edge.Services.Operations;
using NEC.Components.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace Edge.API.Controllers
{
    public class SampleController : ApiController
    {
        private ISampleService _sampleService;
        public SampleController()
        {
            _sampleService = new SampleService();
        }

        // GET api/computeHash?nodeType="Gateway"&nodeName="CentralizeNode"
        [HttpGet]
        public IEnumerable<string> ComputeHash(string nodeType, string requestName)
        {
            DateTime requestStartTime = DateTime.Now;

            LogHelper.WriteDebugLog("ComputeHash");

            Sample sampleRequest = new Sample();
            sampleRequest.NodeName = requestName;
            sampleRequest.NodeType = nodeType;
            sampleRequest.RequestStartTime = requestStartTime;
            sampleRequest.RequestEndTime = DateTime.Now;
            _sampleService.AddSample(sampleRequest);

            if (string.Equals(requestName, "\"CentralizeNode\""))
            {
                string cloudResult = string.Concat(_sampleService.GetSampleFromCloud(), " ", "Using Edge");

                return new string[] { cloudResult };
            }

            return new string[] { "Sample Result" };
        }


    }
}
