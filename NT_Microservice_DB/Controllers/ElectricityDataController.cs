using System.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NT_Microservice_DB.DTO;
using NT_Microservice_DB.Extensions;
using NT_Microservice_DB.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace NT_Microservice_DB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricityDataController : ControllerBase
    {
        private ElectricityContext _electricityContext;
        public ElectricityDataController(ElectricityContext electricityContext)
        {
            _electricityContext = electricityContext;
        }

        // POST: api/ElectricityData
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ElectricityPriceDataDtoIn jsonData)
        {
            if (jsonData == null)
            {
                return BadRequest("Dataa ei vastaanotettu.");
            }

            try
            {
                foreach (var hourPrice in jsonData.Prices)
                {
                    _electricityContext.ElectricityDatas.Add(hourPrice.ToEntity());
                }

                await _electricityContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Virhe tallennettaessa dataa tietokantaan.");
                throw;
            }

            return Ok("Data vastaanotettu ja käsitelty.");

        }
    }
}
