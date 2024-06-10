using PolicyAPI.Models;

namespace PolicyAPI.Repositories
{
    public interface IAddressRepo
    {
        Task<Address> AddAddress(Address Address, string adharCardNo);
        Task<bool> DeleteAddress(string doorNo,string streetName, string adharCardNo);

        Task<Address> GetAddressById(string doorNo, string streetName, string adharCardNo);

        Task<IEnumerable<Address>> GetAllAddresss();

        Task<Address> UpdateAddress(Address Address, string oldDoorNo, string oldStreetName,string adharCardNo);
    }
}
