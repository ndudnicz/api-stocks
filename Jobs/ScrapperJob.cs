using HtmlAgilityPack;

namespace dotnet_api.Jobs;

/// <summary>
/// 
/// </summary>
public class ScrapperJob: IScrapperJob
{
    public void Run(string[] stocks)
    {
        if (stocks is null)
            throw new ArgumentNullException(nameof(stocks));
        var url = "https://live.euronext.com/fr/ajax/getDetailedQuote/";
        var web = new HtmlWeb();
        foreach (var stock in stocks)
        {
            // var doc = web.Load(url + stock);
            Console.WriteLine("getting stock data for " + stock);
            // Scrape stock data
            // Save stock data
        }
    }
}