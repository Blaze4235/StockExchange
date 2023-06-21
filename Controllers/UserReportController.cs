using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StockExchange.Models;
using StockExchange.Models.DbModels;
using StockExchange.Models.RequestModels;

namespace StockExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserReportController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public UserReportController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("UserReport")]
        public IActionResult GetReport()
        {
            var report = _context.User
                .Select(u => new
                {
                    u.username,
                    u.email,
                    u.login,
                    UserBalance = u.UserBalance.Select(fi => new
                    {
                        fi.balance_amount,
                    }),
                    Operations = u.Operations.Select(fi => new
                    {
                        fi.operation_amount,
                    }),
                    FinancialInstruments = u.FinancialInstruments.Select(fi => new
                    {
                        fi.instrument_name,
                        fi.instrument_price,
                        fi.instrument_amount
                    })
                })
                .ToList();

            var reportWithOperationCount = report.Select(item =>
            {
                var operationCount = item.Operations.Count();
                var operationCountText = operationCount == 1 ? "1 операція" : $"{operationCount} операції";

                return new
                {
                    item.username,
                    item.email,
                    item.login,
                    item.UserBalance,
                    OperationCount = operationCountText,
                    item.FinancialInstruments
                };
            });

            return Ok(reportWithOperationCount);
        }

        [HttpPost("getuserbyid")]
        public IActionResult GetUserById(GetUserByIdRequestModel request)
        {
            var userId = request.user_id;

            var user = _context.User
                .Where(u => u.user_id == userId)
                .Select(u => new
                {
                    u.username,
                    u.email,
                    u.login,
                    UserBalance = u.UserBalance.Select(fi => new
                    {
                        fi.balance_amount,
                    }),
                    Operations = u.Operations.Select(fi => new
                    {
                        fi.operation_amount,
                    }),
                    FinancialInstruments = u.FinancialInstruments.Select(fi => new
                    {
                        fi.instrument_name,
                        fi.instrument_price,
                        fi.instrument_amount
                    })
                })
                .FirstOrDefault();

            if (user == null)
            {
                return NotFound("Користувача не знайдено");
            }

            var operationCount = user.Operations.Count();
            var operationCountText = operationCount == 1 ? "1 операція" : $"{operationCount} операції";

            var result = new
            {
                user.username,
                user.email,
                user.login,
                user.UserBalance,
                OperationCount = operationCountText,
                user.FinancialInstruments
            };

            return Ok(result);
        }






    }
}
