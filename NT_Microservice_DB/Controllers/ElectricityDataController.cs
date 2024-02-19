using System;
using System.Collections.Generic;
using System.Data;
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
        ElectricityContext electricitydb;
        public ElectricityDataController()
        {
            electricitydb = new ElectricityContext();
        }

        // POST: api/ElectricityData
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string jsonData)
        {
            try
            {
                // Deserialisoidaan JSON-muotoinen data
                DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(jsonData);
                DataTable dataTable = dataSet.Tables["prices"];

                // Käydään data "rivi" kerrallaan läpi ja lisätään tietokantaan
                foreach (DataRow row in dataTable.Rows)
                {
                    decimal price = Convert.ToDecimal(row["price"]);
                    DateTime startDate = Convert.ToDateTime(row["startDate"]);
                    DateTime endDate = Convert.ToDateTime(row["endDate"]);

                    var electricityData = new ElectricityData
                    {
                        Price = price,
                        StartDate = startDate,
                        EndDate = endDate
                    };

                    // Tallennetaan deserialisoitu data tietokantaan
                    electricitydb.ElectricityDatas.Add(electricityData);
                    await electricitydb.SaveChangesAsync();
                }


                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
