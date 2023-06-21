using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockExchange.Models.DbModels
{
    public class User
    {
        [Key]
        public int user_id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int role_id { get; set; }
        public string login { get; set; }
        [ForeignKey("role_id")]
        public Role Role { get; set; }

        // Навігаційна властивість для колекції FinancialInstrument користувача
        public ICollection<FinancialInstrumnent> FinancialInstruments { get; set; }
        public ICollection<Operations> Operations { get; set; }
        public ICollection<UserBalance> UserBalance{ get; set; }
    }
}
