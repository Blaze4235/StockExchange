/*using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockExchange.Models;
using StockExchange.Models.DbModels;

namespace StockExchange.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public TransactionController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateTransaction(int sender_id, int receiver_id, int instrument_id, int transaction_amount)
        {
            // Отримати дані продавця (sender)
            var sender = _context.User.Find(sender_id);
            if (sender != null)
            {
                _context.Entry(sender).Collection(u => u.FinancialInstruments).Load();
                _context.Entry(sender).Reference(u => u.UserBalance).Load();
            }

            // Отримати дані покупця (receiver)
            var receiver = _context.User.Find(receiver_id);
            if (receiver != null)
            {
                _context.Entry(receiver).Collection(u => u.FinancialInstruments).Load();
                _context.Entry(receiver).Reference(u => u.UserBalance).Load();
            }


            // Перевірити наявність та кількість фінансових інструментів у продавця та покупця
            if (sender != null && receiver != null)
            {
                var instrument = sender.FinancialInstruments.FirstOrDefault(fi => fi.instrument_id == instrument_id);

                if (instrument != null && instrument.instrument_amount >= transaction_amount)
                {
                    // Виконати операцію продажу/покупки

                    // Зменшити кількість фінансового інструменту у продавця
                    instrument.instrument_amount -= transaction_amount;

                    // Збільшити кількість фінансового інструменту у покупця
                    var receiverInstrument = receiver.FinancialInstruments.FirstOrDefault(fi => fi.instrument_id == instrument_id);
                    if (receiverInstrument != null)
                    {
                        receiverInstrument.instrument_amount += transaction_amount;
                    }
                    else
                    {
                        // Створити новий запис фінансового інструменту у покупця
                        receiver.FinancialInstruments.Add(new FinancialInstrument
                        {
                            instrument_id = instrument.instrument_id,
                            instrument_amount = transaction_amount
                        });
                    }

                    // Оновити баланс продавця
                    sender.UserBalance.balance_amount += transaction_amount;

                    // Врахувати комісію
                    var commission = CalculateCommission(transaction_amount);
                    sender.UserBalance.balance_amount -= commission;
                    receiver.UserBalance.balance_amount -= commission;

                    // Оновити час останнього оновлення балансу
                    sender.UserBalance.last_updated = DateTime.Now;
                    receiver.UserBalance.last_updated = DateTime.Now;

                    // Зберегти зміни у базі даних
                    _context.SaveChanges();

                    // Відповісти успішним результатом
                    return Ok("Transaction completed successfully.");
                }
                else
                {
                    // Обробка випадку, коли фінансовий інструмент відсутній або його недостатньо у продавця
                    return BadRequest("Insufficient funds or instrument not available.");
                }
            }

            // Обробка випадку, коли продавець або покупець не знайдені
            return BadRequest("Invalid sender or receiver ID.");
        }

        private decimal CalculateCommission(int transaction_amount)
        {
            // Ваша логіка розрахунку комісії
            // Наприклад, комісія може бути фіксованою сумою або відсотком від транзакційної суми
            // Ви можете замінити цей код на власну реалізацію розрахунку комісії

            // В прикладі буде використано комісію у розмірі 1% від транзакційної суми
            decimal commissionPercentage = 0.01m;
            decimal commission = transaction_amount * commissionPercentage;

            // Округлення комісії до двох знаків після коми
            commission = Math.Round(commission, 2);

            return commission;
        }
    }
}
*/