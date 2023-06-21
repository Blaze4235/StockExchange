using StockExchange.Models.DbModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockExchange.Models.RequestModels
{
    public class UpdateFinancialInstrumentCategoryModel
    {
        public int Category_id { get; set; }
        public string Category_name { get; set; } = string.Empty;

    }
}
