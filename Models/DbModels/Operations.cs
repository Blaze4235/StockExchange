using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockExchange.Models.DbModels
{
    public class Operations
    {
        [Key]
        public int operation_id{ get; set; }
        public int user_id { get; set; }
        public string operation_type { get; set; }
        public int operation_amount { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy H:mm}")]
        public DateTime operation_date { get; set; }

        [ForeignKey("user_id")]
        public User User { get; set; }

    }
    public enum operation_type
    {
        Buy,
        Sell
    }
}
