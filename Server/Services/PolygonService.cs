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
                    volume = e.Value<Double?>("v") ?? null,
                    open = e.Value<Double?>("o") ?? null,
                    close = e.Value<Double?>("c") ?? null,
                    high = e.Value<Double?>("h") ?? null,
                    low = e.Value<Double?>("l") ?? null,
                    date = (new DateTime(1970, 1, 1)).AddMilliseconds(e.Value<long?>("t") ?? 1)
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

                Console.WriteLine("O: " + o.ToString());
                if (o is null) return null;

                try
                {
                    return o.Select(e => new TickerNews
                    {
                        id = e.Value<String>("id") ?? "",
                        title = e.Value<String>("title") ?? "",
                        author = e.Value<String>("author") ?? "",
                        article_url = e.Value<String>("article_url") ?? "",
                        image_url = e.Value<String>("image_url") ?? "",
                        description = e.Value<String>("description") ?? ""
                    }).ToList();
                }
                catch (Exception)
                {
                    Console.WriteLine("Exception! (" + o.Count() + ")");
                    
                }
                
            }

            return null;
        }
    }
}