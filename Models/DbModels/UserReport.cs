using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockExchange.Models.DbModels
{
    public class UserReport
    {
        [Key]
        public int report_id{ get; set; }
       
        public int user_id { get; set; } 
        public string report_text  { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy H:mm}")]
        public DateTime date_created { get; set; }

        [ForeignKey("user_id")]
        public User User { get; set; }

    }
}
