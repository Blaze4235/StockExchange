using StockExchange.Models;
using StockExchange.Models.DbModels;
using StockExchange.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockExchange.Helpers;


namespace StockExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialInstrumentController : Controller
    {
        ApplicationContext appCtx;
        public FinancialInstrumentController(ApplicationContext ctx)
        {
            appCtx = ctx;
        }


        [HttpPost("financialInstrument")]
        public IActionResult AddFinancialInstrument([FromBody] FinancialInstrumentModel financialinstrumentmodel)
        {
            // Check if the user exists
            User user = appCtx.User.FirstOrDefault(u => u.user_id == financialinstrumentmodel.user_id);
            if (user == null)
            {
                return BadRequest(new { errorMessage = "Invalid user_id" });
            }

            FinancialInstrumnent financialInstrument = new FinancialInstrumnent
            {
                user_id = financialinstrumentmodel.user_id,
                instrument_name = financialinstrumentmodel.instrument_name,
                instrument_category_id = financialinstrumentmodel.instrument_category_id,
                instrument_price = financialinstrumentmodel.instrument_price,
                last_updated = financialinstrumentmodel.last_updated,
                instrument_amount = financialinstrumentmodel.instrument_amount,
            };

            appCtx.FinancialInstrument.Add(financialInstrument);
            appCtx.SaveChanges();

            return Ok(new { message = "Financial Instrument added successfully" });
        }
    }
}

