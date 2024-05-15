using Newtonsoft.Json;

namespace TaskManagerGUI.Models
{
    public class StockModel
    {
        [JsonProperty("1. open")]
        public double Open {  get; set; }
        [JsonProperty("2. high")]
        public double High { get; set; }
        [JsonProperty("3. low")]
        public double Low { get; set; }
        [JsonProperty("4. close")]
        public double Close { get; set; }
        [JsonProperty("5. volume")]
        public int Volume { get; set; }
    }
}
