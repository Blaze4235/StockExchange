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
    public class AdminController : Controller
    {
        ApplicationContext appCtx;
        public AdminController(ApplicationContext ctx)
        {
            appCtx = ctx;
        }

        [HttpDelete("deleteUser/{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            User user = appCtx.User.FirstOrDefault(d => d.user_id == id);
            if (user != null)
            {
                appCtx.User.Remove(user);
                appCtx.SaveChanges();
                return Ok();
            }
            return BadRequest(new { errorText = "Invalid user_id" });
        }

        [HttpPost("updateUser")]
        public IActionResult UpdateUser([FromBody] UpdateUser updateUser)
        {
            User user = appCtx.User.FirstOrDefault(d => d.user_id == updateUser.user_id);
            if (user != null)
            {
                user.email = updateUser.email;
                user.username = updateUser.username;
                user.role_id = updateUser.role_id;
                appCtx.SaveChanges();
                return Ok();
            }

            return BadRequest(new { errorText = "Invalid user_id" });
        }

        [HttpGet("getUsers")]
        public IActionResult GetUsers()
        {
            List<User> user = appCtx.User.ToList();
            List<User> fin = new List<User>();
            for (int i = 0; i < user.Count; i++)
            {
                fin.Add(new User()
                {
                    user_id = user[i].user_id,
                    username = user[i].username,
                    email = user[i].email,
                    role_id = user[i].role_id,
                    password = user[i].password,
                    login = user[i].login
                }) ;
            }
            return Json(fin);
        }

        
        [HttpPost("userBalance")]
        public IActionResult AddUserBalance([FromBody] UpdateBalanceModel updateBalanceModel)
        {
             // Check if the user exists
             User user = appCtx.User.FirstOrDefault(u => u.user_id == updateBalanceModel.user_id);
             if (user == null)
             {
                return BadRequest(new { errorMessage = "Invalid user_id" });
             }

             UserBalance userBalance = new UserBalance
             {
                 user_id = updateBalanceModel.user_id,
                 balance_amount = updateBalanceModel.balance_amount,
                 last_updated = updateBalanceModel.last_updated
             };

             appCtx.UserBalance.Add(userBalance);
             appCtx.SaveChanges();

             return Ok(new { message = "User balance added successfully" });

        }
        
        }
        
    }

