using Microsoft.EntityFrameworkCore;
using PolicyAPI.Contexts;
using PolicyAPI.Models;

namespace PolicyAPI.Repositories
{
    public class PolicyHolderRepo : IPolicyHolderRepo
    {
        private PolicyContext _dbContext;

        public PolicyHolderRepo(PolicyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PolicyHolder> AddPolicyHolder(PolicyHolder policyHolder)
        {
            var result=   await this._dbContext.PolicyHolders
                .AddAsync(policyHolder);
           await this._dbContext.SaveChangesAsync();
            return result.Entity;
             
        }

        public async Task<bool> DeletePolicyHolder(string adharCardNo)
        {
            bool status = false;
            PolicyHolder result = await IsPolicyHolderExists(adharCardNo);

            if (result != null)
            {
                this._dbContext.PolicyHolders.Remove(result);
                this._dbContext.SaveChanges();
            }

            result = await IsPolicyHolderExists(adharCardNo);
            if (result == null)
            {
                status = true;
            }
            return status;
        }

        private async Task<PolicyHolder> IsPolicyHolderExists(string adharCardNo)
        {
            return await this._dbContext.PolicyHolders
                 .FirstOrDefaultAsync(p => p.AdharCardNo == adharCardNo);
        }

        public async Task<IEnumerable<PolicyHolder>> GetAllPolicyHolders()
        {
            return await this._dbContext.PolicyHolders.ToListAsync();
        }

        public Task<PolicyHolder> GetPolicyHolderById(string adharCardNo)
        {
            return IsPolicyHolderExists(adharCardNo);
        }

        public async Task<PolicyHolder> UpdatePolicyHolderData(string adharCardNo, string email, long phone)
        {
            var result= await IsPolicyHolderExists(adharCardNo);

            if(result != null)
            {
                result.Email = email;
                result.Phone = phone;
                this._dbContext.SaveChanges();

            }
            return result;
        }
    }
}
