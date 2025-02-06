using dotnet_api.Data.Entities;

namespace dotnet_api.Repositories;

public class StockRepository(): IStockRepository
{
    private static readonly Dictionary<string, Stock> StocksDict = new Dictionary<string, Stock>();

    public IEnumerable<Stock> Get()
    {
        return StocksDict.Values;
    }
    
    public Stock Upsert(Stock stock)
    {
        if (StocksDict.TryGetValue(stock.Isin, out var existingStock))
        {
            existingStock.LastPrice = stock.LastPrice;
            existingStock.Variation = stock.Variation;
            return existingStock;
        }
        StocksDict.Add(stock.Isin, stock);
        return stock;
    }
}