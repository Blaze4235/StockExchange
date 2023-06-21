using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockExchange.Models.RequestModels
{
    public class UserReportModel
    {
        public int Report_id { get; set; }
        public int User_id { get; set; }
        public string Report_text { get; set; } = string.Empty;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy H:mm}")]
        public DateTime Date_created { get; set; }

    }
}
