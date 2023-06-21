using StockExchange.Models.DbModels;

namespace StockExchange.Models.RequestModels
{
    public class UserModel
    {
        public string username { get; set; } 
        public string password { get; set; } 
        public string email { get; set; } 
        public string login { get; set; } 
        public int role_id { get; set; }


    }
}
