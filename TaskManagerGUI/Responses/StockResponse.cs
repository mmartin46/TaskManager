using Newtonsoft.Json;
using TaskManagerGUI.Models;

namespace TaskManagerGUI.Responses
{
    public class StockResponse
    {
        [JsonProperty("Time Series (5min)")]
        public Dictionary<string, StockModel> Stocks { get; set; }
    }
}
