using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PolicyAPI.Repositories;
using System.Text.Json;

namespace PolicyAPI.Controllers
{

    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableCors]
    [ApiController]
    public class PolicyPublisherController : ControllerBase
    {
        private IPolicyPublishRepo _policyPublishRepo;
        private IPolicyRepo _policyRepo;
        private IConfiguration _configuration;

        public PolicyPublisherController(IPolicyPublishRepo policyPublishRepo,
            IPolicyRepo policyRepo, 
            IConfiguration configuration)
        {
            _policyPublishRepo = policyPublishRepo;
            _policyRepo = policyRepo;                
            _configuration = configuration;
        }

        [HttpGet]
        [Route("publish")]

        public async Task<IActionResult> PolicyPublish()
        {
            string TopicName = _configuration["TopicName"];

            var Data = await _policyRepo.GetAllPolicies();

            string Message = JsonSerializer.Serialize(Data);

           return Ok(await _policyPublishRepo.
               PublishPolicyData(TopicName, Message, _configuration));
        }



    }
}
