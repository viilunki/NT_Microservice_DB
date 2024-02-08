using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NT_Microservice_DB.Models;

namespace NT_Microservice_DB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricityDatasController : ControllerBase
    {
        private readonly ElectricityContext _context;

        public ElectricityDatasController(ElectricityContext context)
        {
            _context = context;
        }

        // GET: api/ElectricityDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElectricityData>>> GetElectricityDatas()
        {
            return await _context.ElectricityDatas.ToListAsync();
        }

        // GET: api/ElectricityDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ElectricityData>> GetElectricityData(int id)
        {
            var electricityData = await _context.ElectricityDatas.FindAsync(id);

            if (electricityData == null)
            {
                return NotFound();
            }

            return electricityData;
        }

        // PUT: api/ElectricityDatas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElectricityData(int id, ElectricityData electricityData)
        {
            if (id != electricityData.Id)
            {
                return BadRequest();
            }

            _context.Entry(electricityData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectricityDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ElectricityDatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ElectricityData>> PostElectricityData(ElectricityData electricityData)
        {
            _context.ElectricityDatas.Add(electricityData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetElectricityData", new { id = electricityData.Id }, electricityData);
        }

        // DELETE: api/ElectricityDatas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElectricityData(int id)
        {
            var electricityData = await _context.ElectricityDatas.FindAsync(id);
            if (electricityData == null)
            {
                return NotFound();
            }

            _context.ElectricityDatas.Remove(electricityData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ElectricityDataExists(int id)
        {
            return _context.ElectricityDatas.Any(e => e.Id == id);
        }
    }
}
