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
using System.IO;


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

        [HttpGet("getprices")]
        public async Task<IActionResult> Get([FromQuery] DateTime startDate, DateTime endDate)
        {
            var results = await _electricityContext.ElectricityDatas
                .Where(x => x.StartDate >= startDate && x.EndDate <= endDate).ToListAsync();

            return Ok(results);
        }

        [HttpGet("GetElectricityPricesFromRange")]
        public async Task<IActionResult> Get([FromQuery] DateTime startDate, DateTime endDate, int pageSize, int Page)
        {
            var results = await _electricityContext.ElectricityDatas
                .Where(x => x.StartDate >= startDate && x.EndDate <= endDate)
                .Skip((Page - 1)*pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(results);
        }

        [HttpGet("GetPriceDifferenceFromRange")]
        public async Task<IActionResult> Get([FromQuery] DateTime startDate, DateTime endDate, decimal fixedPrice)
        {
            var prices = await _electricityContext.ElectricityDatas
                .Where(x => x.StartDate >= startDate && x.EndDate <= endDate)
                .Select(x => x.Price)
                .ToListAsync();

            decimal priceSum = 0;

            foreach (decimal price in prices){
                priceSum += price;
            }

            decimal fixedPriceTotal = prices.Count() * fixedPrice;
            decimal priceDifference = priceSum - fixedPriceTotal;


            if (priceDifference > 0)
            {
                return Ok("Pörssisähkö on kalliimpi annetulla aikavälillä. Pörssisähkön kokonaishinta on " + priceSum.ToString() + " ja kiinteän sähkön kokonaishinta on " + fixedPriceTotal.ToString());
            }
            else if (priceDifference == 0) {
                return Ok("Pörssisähkö ja kiinteä sähkö ovat annetalla aikavälillä saman hintaisia. Sähkön kokonaishinta on " + priceSum.ToString());
            }
            else
            {
                return Ok("Pörssisähkö on halvempi annetulla aikavälillä. Pörssisähkön kokonaishinta on " + priceSum.ToString() + " ja kiinteän sähkön kokonaishinta on " + fixedPriceTotal.ToString());
            }
        }

    }


}
