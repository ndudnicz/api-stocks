using dotnet_api.Data.Entities;
using dotnet_api.Hubs;
using dotnet_api.Repositories;
using HtmlAgilityPack;
using Microsoft.AspNetCore.SignalR.Client;

namespace dotnet_api.Jobs;

/// <summary>
/// 
/// </summary>
public class ScrapperJob(IStockRepository stockRepository, IHubConnectionHandler hubConnectionHandler): IScrapperJob
{
    public async Task Run(string[] stocksIsin)
    {
        if (stocksIsin is null)
            throw new ArgumentNullException(nameof(stocksIsin));
        var url = "https://live.euronext.com/fr/ajax/getDetailedQuote/";
        var web = new HtmlWeb();
        var parsedStocks = new List<Stock>();
        foreach (var isin in stocksIsin)
        {
            Console.WriteLine("fetching stock data for " + isin);
            
            var doc = web.Load(url + isin + "-XPAR");
            var parsedStock = new Stock()
            {
                Name = doc.GetElementbyId("header-instrument-name").InnerText.Trim(['\t','\n']),
                Isin = isin,
                Exchange = "Euronext Paris",
                Variation = double.Parse(doc.DocumentNode.SelectNodes("//span[@class='text-ui-grey-1 mr-2']")[1].InnerText.Trim(['(', ')', '+', '%']).Replace(',', '.')),
                LastPrice = double.Parse(doc.GetElementbyId("header-instrument-price").InnerText.Replace(',', '.'))
            };
            stockRepository.Upsert(parsedStock);
            parsedStocks.Add(parsedStock);
        }
        await hubConnectionHandler.HubConnection.SendAsync("NewMessage", parsedStocks);
    }
}