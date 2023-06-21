using System;
using System.ComponentModel.DataAnnotations;

namespace StockExchange.Models.RequestModels
{
    public class TransactionModel
    {
        public int sender_id { get; set; }
        public int user_id { get; set; }
        public int receiver_id { get; set; }
        public int transaction_amount { get; set; }
        public string instrument_name { get; set; }
        public int instrument_amount { get; set; }
        public int instrument_price { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy H:mm}")]
        public DateTime transaction_date { get; set; }
    }
}
