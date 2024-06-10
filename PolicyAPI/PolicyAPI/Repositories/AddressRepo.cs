using Microsoft.EntityFrameworkCore;
using PolicyAPI.Contexts;
using PolicyAPI.Models;

namespace PolicyAPI.Repositories
{
    public class AddressRepo : IAddressRepo
    {
        private PolicyContext _dbContext;

        public AddressRepo(PolicyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Address> AddAddress(Address Address, string adharCardNo)
        {
            PolicyHolder policyHolder = await IsPolicyHolderExists(adharCardNo);

            Address.PolicyHolder = policyHolder;
            var result = await this._dbContext.Addresses
                .AddAsync(Address);
            await this._dbContext.SaveChangesAsync();
            return result.Entity;

        }

        private async Task<PolicyHolder> IsPolicyHolderExists(string adharCardNo)
        {
            return await this._dbContext.PolicyHolders
                 .FirstOrDefaultAsync(p => p.AdharCardNo == adharCardNo);
        }
        public async Task<bool> DeleteAddress(string doorNo, string streetName, string adharCardNo)
        {
            bool status = false;
            Address result = await IsAddressExists(doorNo, streetName, adharCardNo);

            if (result != null)
            {
                this._dbContext.Addresses.Remove(result);
                this._dbContext.SaveChanges();
            }

            result = await IsAddressExists(doorNo, streetName, adharCardNo);
            if (result == null)
            {
                status = true;
            }
            return status;
        }

        private async Task<Address> IsAddressExists(string doorNo, string streetName, string adharCardNo)
        {
            return await this._dbContext.Addresses
                 .FirstOrDefaultAsync(p => (p.AdharCardNo == adharCardNo) && 
                 (p.StreetName==streetName) &&(p.DoorNo==doorNo));
        }

        public async Task<IEnumerable<Address>> GetAllAddresss()
        {
            return await this._dbContext.Addresses.ToListAsync();
        }

        public Task<Address> GetAddressById(string doorNo, string streetName, string adharCardNo)
        {
            return IsAddressExists(doorNo, streetName, adharCardNo);
        }

        public async Task<Address> UpdateAddress(Address address, string oldDoorNo, 
            string oldStreetName, string adharCardNo)
        {
            var result = await IsAddressExists(oldDoorNo,oldStreetName, adharCardNo);

            if (result != null)
            {
                result.DoorNo = address.DoorNo;
                result.StreetName = address.StreetName;
                result.City = address.City;
                result.State = address.State;
                result.Country  = address.Country;

                this._dbContext.SaveChanges();

            }
            return result;
        }
    }
}

