using PolicyAPI.Models;

namespace PolicyAPI.Repositories
{
    public interface IPolicyHolderRepo
    {
        Task<PolicyHolder> AddPolicyHolder(PolicyHolder policyHolder);
        Task<bool> DeletePolicyHolder(string adharCardNo);

        Task<PolicyHolder> GetPolicyHolderById(string adharCardNo);

        Task<IEnumerable<PolicyHolder>> GetAllPolicyHolders();

        Task<PolicyHolder> UpdatePolicyHolderData(string adharCardNo,
            string email,long phone);

    }
}
