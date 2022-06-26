using System;
using System.Text.Json;
using APBD_PRO.Server.Models;
using APBD_PRO.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APBD_PRO.Server.Services
{
	public class PolygonService : IPolygonService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public PolygonService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("Polygon.io");
        }

        public async Task<Aggregates> GetAggregates(string stocksTicker)
        {
            
            //TODO - change query to be flexible
            var httpResponseMessage = await _httpClient.GetAsync($"v2/aggs/ticker/{stocksTicker}/range/1/day/2021-07-22/2021-07-22");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                Aggregates aggregates = await System.Text.Json.JsonSerializer.DeserializeAsync<Aggregates>(contentStream);

                Console.WriteLine(aggregates.ticker);

                return aggregates;
            }
            return null;
        }

        public async Task<IEnumerable<BasicTicker>> GetBasicTickers(string ticker)
        {
            var httpResponse = await _httpClient.GetAsync($"v3/reference/tickers?search={ticker}&active=true&sort=ticker&order=asc&limit=10");

            if (httpResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("HELLO FROM ME: " + ticker);
                using var contentStream = await httpResponse.Content.ReadAsStreamAsync();
                var res = await System.Text.Json.JsonSerializer.DeserializeAsync<Tickers>(contentStream);
                return res.results;
                //return res.results;


                //    IEnumerable<BasicTicker> result = new List<BasicTicker>();

                //    var serializer = new Newtonsoft.Json.JsonSerializer();

                //    using (var contentStream = await httpResponse.Content.ReadAsStreamAsync())
                //    using (var reader = new StreamReader(contentStream))
                //    using (var jsonReader = new JsonTextReader(reader))
                //    {
                //        jsonReader.SupportMultipleContent = true;

                //        while (jsonReader.Read())
                //        {
                //            result.Append(serializer.Deserialize<BasicTicker>(jsonReader));
                //        }

                //        return result;
                //    }
            }
            return null;
        }

        
        public async Task<FullTicker> GetFullTicker(string ticker)
        {
            var httpResponse = await _httpClient.GetAsync($"v3/reference/tickers/{ticker}");

            if (httpResponse.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponse.Content.ReadAsStreamAsync();

                var res = await System.Text.Json.JsonSerializer.DeserializeAsync<TickerDetailsResponse>(contentStream);

                return res.results;
            }
            return null;
        }

        public async Task<IEnumerable<ChartData>> GetChartData(string ticker, string from, string to)
        {
            var httpResponse = await _httpClient.GetAsync($"v2/aggs/ticker/{ticker}/range/1/day/{from}/{to}");

            if (httpResponse.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponse.Content.ReadAsStreamAsync();
                string jsonText = await new StreamReader(contentStream).ReadToEndAsync();

                var o = JObject.Parse(jsonText).SelectToken("results");

                //Console.WriteLine("O: " + o.ToString());

                return o.Select(e => new ChartData
                {
                    Volume = e["v"].ToObject<Double>(),
                    Open = e["o"].ToObject<Double>(),
                    Close = e["c"].ToObject<Double>(),
                    High = e["h"].ToObject<Double>(),
                    Low = e["l"].ToObject<Double>(),
                    Date = (new DateTime(1970, 1, 1)).AddMilliseconds(e["t"].ToObject<double>())
                }).ToList();
            }

            return null;
        }

        public async Task<IEnumerable<TickerNews>> GetTickerNews(string ticker)
        {
            var httpResponse = await _httpClient.GetAsync($"v2/reference/news?ticker={ticker}");

            if (httpResponse.IsSuccessStatusCode)
            {

                using var contentStream = await httpResponse.Content.ReadAsStreamAsync();
                string jsonText = await new StreamReader(contentStream).ReadToEndAsync();

                var o = JObject.Parse(jsonText).SelectToken("results");

                //Console.WriteLine("O: " + o.ToString());
                return o.Select(e => new TickerNews
                {
                    title = e["title"].ToObject<String>(),
                    author = e["author"].ToObject<String>(),
                    article_url = e["article_url"].ToObject<String>(),
                    image_url = e["image_url"].ToObject<String>(),
                    description = e["description"].ToObject<String>()
                }).ToList();
                
            }

            return null;
        }
    }
}