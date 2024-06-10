using Microsoft.EntityFrameworkCore;
using PolicyAPI.Contexts;
using PolicyAPI.Models;

namespace PolicyAPI.Repositories
{
    public class VehicleRepo : IVehicleRepo
    {
        private PolicyContext _dbContext;

        public VehicleRepo(PolicyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Vehicle> AddVehicle(Vehicle Vehicle)
        {
            var result = await this._dbContext.Vehicles
                .AddAsync(Vehicle);
            await this._dbContext.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<bool> DeleteVehicle(string registrationNo)
        {
            bool status = false;
            Vehicle result = await IsVehicleExists(registrationNo);

            if (result != null)
            {
                this._dbContext.Vehicles.Remove(result);
                this._dbContext.SaveChanges();
            }

            result = await IsVehicleExists(registrationNo);
            if (result == null)
            {
                status = true;
            }
            return status;
        }

        private async Task<Vehicle> IsVehicleExists(string registrationNo)
        {
            return await this._dbContext.Vehicles
                 .FirstOrDefaultAsync(p => p.RegistrationNo == registrationNo);
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehicles()
        {
            return await this._dbContext.Vehicles.ToListAsync();
        }

        public Task<Vehicle> GetVehicleById(string registrationNo)
        {
            return IsVehicleExists(registrationNo);
        }

        
    }
}
