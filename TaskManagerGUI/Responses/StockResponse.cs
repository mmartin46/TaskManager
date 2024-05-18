using Newtonsoft.Json;
using TaskManagerGUI.Models;

namespace TaskManagerGUI.Responses
{
    public class StockResponse
    {
        [JsonProperty("Time Series (Daily)")]
        public Dictionary<string, StockModel> Stocks { get; set; }
    }
}
