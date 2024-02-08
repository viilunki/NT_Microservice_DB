using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NT_Microservice_DB.Models;

namespace NT_Microservice_DB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricityDataController : ControllerBase
    {
        private readonly ElectricityContext _context;

        public ElectricityDataController(ElectricityContext context)
        {
            _context = context;
        }

        // GET: api/ElectricityData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElectricityData>>> GetElectricityItems()
        {
            return await _context.ElectricityDatas.ToListAsync();
        }


        // POST: api/ElectricityData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ElectricityData>> PostElectricityData([FromBody] ElectricityDataRequestModel requestModel)
        {
            try
            {
                // Käsittele requestModel.prices -lista
                foreach (var item in requestModel.prices)
                {
                    var electricityData = new ElectricityData
                    {
                        price = item.price,
                        startDate = item.startDate,
                        endDate = item.endDate
                    };

                    _context.ElectricityDatas.Add(electricityData);
                }

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Virhe tallennettaessa dataa tietokantaan: {ex.Message}");
            }
        }




        private bool ElectricityItemExists(int id)
        {
            return _context.ElectricityDatas.Any();
        }
    }
}
