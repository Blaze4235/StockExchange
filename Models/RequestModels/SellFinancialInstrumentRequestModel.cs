namespace StockExchange.Models.RequestModels
{
    public class SellFinancialInstrumentRequestModel
    {
        public int user_id { get; set; }
        public string instrument_name { get; set; }
        public int instrument_category_id { get; set; }
        public int instrument_price { get; set; }
        public DateTime last_ipdated { get; set; }
        public int instrument_amount { get; set; }
        public int instrument_id { get; set; }
    }
}
