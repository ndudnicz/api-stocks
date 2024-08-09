using dotnet_api.Repositories;

namespace dotnet_api.Services;

public class HomeService(IDataRepository dataRepository): IHomeService
{
    private readonly IDataRepository _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
    
    public IEnumerable<string> Get()
    {
        return _dataRepository.Get();
    }
    
    public IEnumerable<string> Post(string str)
    {
        return _dataRepository.Post(str);
    }
}