using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockExchange.Models.DbModels
{
    public class Transaction
    {
        [Key]
        public int transaction_id { get; set; }

        public int user_id { get; set; }
        public int transaction_amount { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy H:mm}")]
        public DateTime transaction_date { get; set; }

        [ForeignKey("user_id")]
        public User user { get; set; }

        [ForeignKey("sender_id")]
        public User sender { get; set; }

        [ForeignKey("receiver_id")]
        public User receiver { get; set; }

        public int sender_id { get; set; }
        public int receiver_id { get; set; }

    }
}
