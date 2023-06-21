using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockExchange.Models.DbModels
{
    public class FinancialInstrumnent
    {
        [Key]
        public int instrument_id{ get; set; }
        public string instrument_name { get; set; }
        public int instrument_category_id { get; set; }
        public int instrument_price { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy H:mm}")]
        public DateTime last_updated { get; set; }

        public int user_id { get; set; }
        public int instrument_amount { get; set; }

        [ForeignKey("user_id")]
        public User User { get; set; }
        [ForeignKey("instrument_category_id")]
        public FinancialInstrumnentCategory FinancialInstumentCategory { get; set; }

    }
}
