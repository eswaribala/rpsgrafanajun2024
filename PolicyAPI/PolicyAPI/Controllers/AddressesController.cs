using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolicyAPI.Contexts;
using PolicyAPI.DTOs;
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
    public class AddressesController : ControllerBase
    {
        private readonly IAddressRepo _addressRepo;

        public AddressesController(IAddressRepo addressRepo)
        {
            _addressRepo = addressRepo;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<IEnumerable<Address>> GetAddresses()
        {
            return await _addressRepo.GetAllAddresss();
        }

        // GET: api/Addresses/5
        [HttpPut]
        public async Task<ActionResult<Address>> GetAddress( 
        AddressRequest addressRequest)
        {
            var address = await _addressRepo.GetAddressById(addressRequest.DoorNo, 
                addressRequest.StreetName, addressRequest.AdharCardNo);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{oldDoorNo}/{oldStreetName}/{adharCardNo}")]
        public async Task<IActionResult> PutAddress([FromBody] Address address, 
            string oldDoorNo,string oldStreetName,string adharCardNo)
        {
            var result=await _addressRepo.UpdateAddress(address,oldDoorNo,oldStreetName,adharCardNo);


            return CreatedAtAction("GetAddresses", new { id = result.AddressId }, result);

        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{adharCardNo}")]
        public async Task<ActionResult<Address>> PostAddress(Address address,string adharCardNo)
        {
            var result = await _addressRepo.AddAddress(address,adharCardNo);


            return CreatedAtAction("GetAddresses", new { id = result.AddressId }, result);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{doorNo}/{streetName}/{adharCardNo}")]
        public async Task<IActionResult> DeleteAddress(string doorNo,
            string streetName, string adharCardNo)
        {
            if(await _addressRepo.DeleteAddress(doorNo, streetName, adharCardNo))
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }

    }
}
