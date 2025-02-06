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
        foreach (var isin in stocksIsin)
        {
            Console.WriteLine("fetching stock data for " + isin);
            
            var doc = web.Load(url + isin + "-XPAR");
            var parsedStock = new Stock()
            {
                Name = doc.GetElementbyId("header-instrument-name").InnerText.Trim('\t').Trim('\n'),
                Isin = isin,
                Exchange = "Euronext Paris",
                Variation = doc.DocumentNode.SelectNodes("//span[@class='text-ui-grey-1 mr-2']")[1].InnerText,
                LastPrice = doc.GetElementbyId("header-instrument-price").InnerText
            };
            stockRepository.Upsert(parsedStock);
            await hubConnectionHandler.HubConnection.SendAsync("NewMessage", parsedStock);
        }
    }
}