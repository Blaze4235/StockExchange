using System.ComponentModel.DataAnnotations;

namespace StockExchange.Models.DbModels
{
    public class Role
    {
        [Key]
        public int role_id { get; set; }
        
        public string role_name { get; set; }

        public List<User> User { get; set; }

    }
}
