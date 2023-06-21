using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockExchange.Models.DbModels
{
    public class FinancialInstrumnentCategory
    {
        [Key]
        public int category_id{ get; set; }
        public string category_name { get; set; }

    }
}
