namespace StockExchange.Models.RequestModels
{
    public class UpdateUser
    {
        public int user_id { get; set; }
        public string email { get; set; } 
        public string username { get; set; } 

        public string login { get; set; }
        public int role_id { get; set; }
    }
}
