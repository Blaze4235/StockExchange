using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockExchange.Models.DbModels
{
    public class UserBalance
    {
        [Key]
        public int balance_id { get; set; }
        public int user_id { get; set; }
        public int balance_amount { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy H:mm}")]
        public DateTime last_updated { get; set; }

        [ForeignKey("user_id")]
        public User User { get; set; }

        public UserBalance()
        {
        }

        public UserBalance(int userId, int balanceAmount)
        {
            user_id = userId;
            balance_amount = balanceAmount;
            last_updated = DateTime.Now;
        }
    }
}
