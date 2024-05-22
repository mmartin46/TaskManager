using Newtonsoft.Json;
using static TaskManagerGUI.Controllers.StockController;
using TaskManagerGUI.Models;
using TaskManagerGUI.Responses;
using CsvHelper;
using System.Globalization;

namespace TaskManagerGUI.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly IConfiguration _configuration;
        public StockRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public async Task<Dictionary<string, StockModel>> ProcessStockApi(string company, int minutes = 5)
        {
            string QueryUrl = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={company}&outputsize=full&apikey=K848YE2HT6B4E53P";
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

        public async Task<CompanyModel> GetEarnings(string ?company)
        {
            string companyToCheck;
            if (company == null) 
            {
                companyToCheck = _configuration.GetValue<string>("DefaultCompany");
            }
            else
            {
                companyToCheck = company;
            }


            string QueryUrl = $"https://www.alphavantage.co/query?function=EARNINGS_CALENDAR&horizon=3month&apikey=demo";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(QueryUrl);
            if (response.IsSuccessStatusCode)
            {
                using (var data = await response.Content.ReadAsStreamAsync())
                {
                    var reader = new StreamReader(data);
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<dynamic>();


                        foreach (var record in records)
                        {
                            if (record.symbol.StartsWith(companyToCheck[0]) && record.symbol.Equals(companyToCheck))
                            {

                                string currency = record.currency;
                                string estimate = (record.estimate);
                                string name = record.name;
                                DateTime fiscalDateEnding = DateTime.Parse(record.fiscalDateEnding);
                                DateTime reportDate = DateTime.Parse(record.reportDate);

                                return new CompanyModel
                                {
                                    Currency = currency,
                                    Name = name,
                                    Estimate = estimate,
                                    FiscalDateEnding = fiscalDateEnding,
                                    ReportDate = reportDate
                                };
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
            return null;
        }
 
    }
}
