using StockExchange.Models.DbModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockExchange.Models.RequestModels
{
    public class FinancialInstrumentModel
    {
        public int instrument_id { get; set; }
        public string instrument_name { get; set; } 
        public int instrument_category_id { get; set; }
        public int instrument_price { get; set; }
        public DateTime last_updated { get; set; }
        public int user_id { get; set; }
        public int instrument_amount { get; set; }

    }
}
