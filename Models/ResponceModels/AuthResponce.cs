using StockExchange.Models.DbModels;
using System.Xml.Linq;

namespace StockExchange.Models.ResponceModels
{
        public class AuthResponse
        {
        public string username { get; set; }
        public string login { get; set; }
        public string password { get; set; } 
        public string email { get; set; } 
        public int role_id { get; set; }

        public string Token { get; set; }

        public AuthResponse(User user, string token)
        { 
        
            username = user.username;
            role_id = user.role_id;
            email = user.email;
            login = user.login;
            password = user.password;
            Token = token;
        }
    }

}
