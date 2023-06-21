using StockExchange.Models.DbModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockExchange.Models.RequestModels
{
    public class OperationsModel
    {
        public int Operation_id { get; set; }
        public int User_id { get; set; }
        public string Operation_type { get; set; } = string.Empty;
        public int Operation_amount { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy H:mm}")]
        public DateTime Operation_date { get; set; }

    }
}
