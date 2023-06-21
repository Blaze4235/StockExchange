using StockExchange.Models.DbModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockExchange.Models.RequestModels
{
    public class UpdateBalanceModel
    {
        public int balance_id { get; set; }
        public int user_id { get; set; }
        public int balance_amount { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy H:mm}")]
        public DateTime last_updated { get; set; }


    }
}
