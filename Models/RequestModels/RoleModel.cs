using StockExchange.Models.DbModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockExchange.Models.RequestModels
{
    public class RoleModel
    {
        public int Role_id { get; set; }
        public string Role_name { get; set; } = string.Empty;

    }
}
