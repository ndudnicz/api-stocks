using dotnet_api.Data.Entities;

namespace dotnet_api.Repositories;

public interface IStockRepository
{
    public IEnumerable<Stock> Get();
    public Stock Upsert(Stock element);
}