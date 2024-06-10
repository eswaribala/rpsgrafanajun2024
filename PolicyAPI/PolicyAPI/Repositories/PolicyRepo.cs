using Microsoft.EntityFrameworkCore;
using PolicyAPI.Contexts;
using PolicyAPI.Models;

namespace PolicyAPI.Repositories
{
    public class PolicyRepo : IPolicyRepo
    {
        private PolicyContext _dbContext;

        public PolicyRepo(PolicyContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Policy> AddPolicy(Policy policy, string adharCardNo, string registrationNo)
        {
            PolicyHolder policyHolder = await IsPolicyHolderExists(adharCardNo);
            Vehicle vehicle=  await IsVehicleExists(registrationNo);
            policy.PolicyHolder = policyHolder;
            policy.Vehicle = vehicle;

             var result= await this._dbContext.Policies.AddAsync(policy);
            await this._dbContext.SaveChangesAsync();
            return result.Entity;
        }
        private async Task<PolicyHolder> IsPolicyHolderExists(string adharCardNo)
        {
            return await this._dbContext.PolicyHolders
                 .FirstOrDefaultAsync(p => p.AdharCardNo == adharCardNo);
        }
        private async Task<Vehicle> IsVehicleExists(string registrationNo)
        {
            return await this._dbContext.Vehicles
                 .FirstOrDefaultAsync(p => p.RegistrationNo == registrationNo);
        }
        public async Task<bool> DeletePolicy(long PolicyNo)
        {
            bool status = false;

            Policy result = await IsPolicyExists(PolicyNo);
            if(result==null)
            {
                this._dbContext.Policies.Remove(result);
                status = true;
            }
            return status;
            
        }

        private async Task<Policy> IsPolicyExists(long policyNo)
        {
            return await this._dbContext.Policies
                 .FirstOrDefaultAsync(p => p.PolicyNo == policyNo);
        }

        public async Task<IEnumerable<Policy>> GetAllPolicies()
        {
            return await this._dbContext.Policies
                .Include(p=>p.PolicyHolder)
               .Include(p=>p.Vehicle)
                .ToListAsync();
        }

        public Task<Policy> GetPolicyByPolicyNo(long PolicyNo)
        {
            return IsPolicyExists(PolicyNo);
        }
    }
}
