using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolicyAPI.Contexts;
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
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepo _vehicleRepo;

        public VehiclesController(IVehicleRepo vehicleRepo)
        {
            _vehicleRepo = vehicleRepo;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
            return await _vehicleRepo.GetAllVehicles();
        }

        // GET: api/Vehicles/5
        [HttpGet("{registrationNo}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(string registrationNo)
        {
            var vehicle = await _vehicleRepo.GetVehicleById(registrationNo);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

       

        // POST: api/Vehicles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle([FromBody] Vehicle vehicle)
        {
           var result= await _vehicleRepo.AddVehicle(vehicle);

            return CreatedAtAction("GetVehicles", new { id = result.RegistrationNo }, result);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{registrationNo}")]
        public async Task<IActionResult> DeleteVehicle(string registrationNo)
        {
            if (await _vehicleRepo.DeleteVehicle(registrationNo))
            {
                return new OkResult();
            }
            else
                return new BadRequestResult();
        }

    }
}
