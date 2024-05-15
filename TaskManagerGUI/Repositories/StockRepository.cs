using Newtonsoft.Json;
using static TaskManagerGUI.Controllers.StockController;
using TaskManagerGUI.Models;
using TaskManagerGUI.Responses;

namespace TaskManagerGUI.Repositories
{
    public class StockRepository
    {

        public async Task<Dictionary<string, StockModel>> ProcessStockApi(string company, int minutes=5)
        {
            string QueryUrl = $"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={company}&interval={minutes}min&apikey=demo";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(QueryUrl);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var stockData = JsonConvert.DeserializeObject<StockResponse>(data);
                var results = stockData.Stocks;
                return results;
            }
            else
            {
                return null;
            }
        }
    }
}
