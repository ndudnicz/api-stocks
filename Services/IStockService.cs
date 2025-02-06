using dotnet_api.Data.Entities;

namespace dotnet_api.Services;

public interface IStockService
{
    public IEnumerable<Stock> Get();
    public Stock Upsert(Stock stock);
}