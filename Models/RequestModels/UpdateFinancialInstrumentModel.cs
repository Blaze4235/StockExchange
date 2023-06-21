using StockExchange.Models.DbModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockExchange.Models.RequestModels
{
    public class UpdateFinancialInstrumentModel
    {
        public int Instrument_id { get; set; }
        public string Instrument_name { get; set; } = string.Empty;
        public int Instrument_price { get; set; }
        public int Instrument_amount { get; set; }

    }
}
