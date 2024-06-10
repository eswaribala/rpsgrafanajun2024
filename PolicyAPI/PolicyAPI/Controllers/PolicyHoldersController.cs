using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolicyAPI.Auth;
using PolicyAPI.Models;
using PolicyAPI.Repositories;

namespace PolicyAPI.Controllers
{

    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableCors]
    [ApiController]
    public class PolicyHoldersController : ControllerBase
    {
        private IPolicyHolderRepo _policyHolderRepo;

        public PolicyHoldersController(IPolicyHolderRepo policyHolderRepo)
        {
            _policyHolderRepo = policyHolderRepo;
        }


        [HttpGet]
        [Authorize(Roles= "Admin,User")]
       
        public async Task<IEnumerable<PolicyHolder>> Get()
        {
           return await this._policyHolderRepo.GetAllPolicyHolders();
        }

        [HttpPost]

        public async Task<IActionResult> Post([FromBody] PolicyHolder policyHolder)
        {
            await this._policyHolderRepo.AddPolicyHolder(policyHolder);

            return CreatedAtAction(nameof(Get),
                       new { id = policyHolder.AdharCardNo }, policyHolder);
        }



        [HttpGet("{adharCardNo}")]
        public async Task<PolicyHolder> Get(string adharCardNo)
        {
            return await _policyHolderRepo.GetPolicyHolderById(adharCardNo);
        }


        
        [HttpPut("{adharCardNo}/{email}/{phone}")]
        public async Task<IActionResult> Put(string adharCardNo, string email,long phone)
        {
            var result = await _policyHolderRepo.UpdatePolicyHolderData(adharCardNo, email, phone); 
            return CreatedAtAction(nameof(Get),
                         new { id = result.AdharCardNo}, result);
        }

       
        [HttpDelete("{adharCardNo}")]
        public async Task<IActionResult> Delete(string adharCardNo)
        {
            if (await _policyHolderRepo.DeletePolicyHolder(adharCardNo))
                return new OkResult();
            else
                return new BadRequestResult();
        }
    }
}
