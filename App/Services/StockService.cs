using dotnet_api.Data.Entities;
using dotnet_api.Repositories;

namespace dotnet_api.Services;

public class StockService(IStockRepository stockRepository): IStockService
{
    private readonly IStockRepository _stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
    
    public IEnumerable<Stock> Get()
    {
        return _stockRepository.Get();
    }
    
    public Stock Upsert(Stock stock)
    {
        return _stockRepository.Upsert(stock);
    }
}