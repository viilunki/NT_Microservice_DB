using System.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NT_Microservice_DB.DTO;
using NT_Microservice_DB.Extensions;
using NT_Microservice_DB.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;


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
                    bool alreadyExists = await CheckIfPriceExistsInDb(hourPrice);

                    if (alreadyExists)
                        continue;
                        

                    _electricityContext.ElectricityDatas.Add(hourPrice.ToEntity());
                }

                await _electricityContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Virhe tallennettaessa dataa tietokantaan.");
                throw;
            }

            return Ok("Data vastaanotettu ja käsitelty.");

        }

        private async Task<bool> CheckIfPriceExistsInDb(PriceInfo priceInfo)
        {
            return await _electricityContext.ElectricityDatas.AnyAsync(x => x.StartDate == priceInfo.StartDate && x.EndDate == priceInfo.EndDate);
        }
    }

    
}
