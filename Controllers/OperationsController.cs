using Microsoft.AspNetCore.Mvc;
using StockExchange.Models;
using StockExchange.Models.RequestModels;
using StockExchange.Models.DbModels;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StockExchange.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperationsController : ControllerBase
    {
        private readonly ApplicationContext _appCtx;

        public OperationsController(ApplicationContext appCtx)
        {
            _appCtx = appCtx;
        }

        [HttpPost("buy")]
        public async Task<IActionResult> BuyFinancialInstrument(BuyFinancialInstrumentRequestModel request)
        {
            try
            {
                var userBalance = await _appCtx.UserBalance.FirstOrDefaultAsync(x => x.user_id == request.user_id);
                if (userBalance == null)
                    return BadRequest("Баланс користувача не знайдено");

                var totalPrice = request.instrument_price * request.instrument_amount;
                if (totalPrice > userBalance.balance_amount)
                    return BadRequest("Недостатній баланс");

                // Оновлення балансу користувача
                userBalance.balance_amount -= totalPrice;
                userBalance.last_updated = DateTime.Now;

                _appCtx.UserBalance.Update(userBalance);

                // Пошук або створення фінансового інструменту
                var instrument = await _appCtx.FinancialInstrument.FirstOrDefaultAsync(x => x.instrument_name == request.instrument_name && x.user_id == request.user_id);
                if (instrument == null)
                {
                    instrument = new FinancialInstrumnent
                    {
                        user_id = request.user_id,
                        instrument_name = request.instrument_name,
                        instrument_category_id = request.instrument_category_id,
                        instrument_price = request.instrument_price,
                        instrument_amount = request.instrument_amount,
                        last_updated = DateTime.Now // Встановлюємо поточний час
                    };
                    _appCtx.FinancialInstrument.Add(instrument);
                }
                else
                {
                    instrument.instrument_amount += request.instrument_amount;
                    instrument.last_updated = DateTime.Now; // Оновлюємо поточний час
                }

                // Додавання запису про операцію
                var operation = new Operations
                {
                    user_id = request.user_id,
                    operation_type = operation_type.Buy.ToString(),
                    operation_amount = request.instrument_amount,
                    operation_date = DateTime.Now
                };
                _appCtx.Operations.Add(operation);

                await _appCtx.SaveChangesAsync();

                return Ok("Фінансовий інструмент успішно куплений");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Під час покупки фінансового інструменту виникла помилка: " + ex.Message);
            }
        }


        [HttpPost("sell")]
        public async Task<IActionResult> SellFinancialInstrument(SellFinancialInstrumentRequestModel request)
        {
            try
            {
                var userBalance = await _appCtx.UserBalance.FirstOrDefaultAsync(x => x.user_id == request.user_id);
                if (userBalance == null)
                    return BadRequest("Баланс користувача не знайдено");

                var instrument = await _appCtx.FinancialInstrument.FirstOrDefaultAsync(x => x.instrument_name == request.instrument_name && x.user_id == request.user_id);
                if (instrument == null)
                    return BadRequest("Інструмент не знайдено");

                if (request.instrument_amount > instrument.instrument_amount)
                    return BadRequest("Недостатня кількість інструментів");

                var totalPrice = request.instrument_price * request.instrument_amount;

                // Оновлення балансу користувача
                userBalance.balance_amount += totalPrice;
                userBalance.last_updated = DateTime.Now;

                _appCtx.UserBalance.Update(userBalance);

                // Оновлення таблиці FinancialInstrument
                instrument.instrument_amount -= request.instrument_amount;
                instrument.last_updated = DateTime.Now; // Оновлюємо поточний час

                // Додавання запису про операцію
                var operation = new Operations
                {
                    user_id = request.user_id,
                    operation_type = operation_type.Sell.ToString(),
                    operation_amount = request.instrument_amount,
                    operation_date = DateTime.Now
                };
                _appCtx.Operations.Add(operation);

                await _appCtx.SaveChangesAsync();

                return Ok("Фінансовий інструмент успішно проданий");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Під час продажу фінансового інструменту виникла помилка: " + ex.Message);
            }
        }



    }
}
