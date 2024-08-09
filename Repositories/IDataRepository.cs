namespace dotnet_api.Repositories;

public interface IDataRepository
{
    public IEnumerable<string> Get();
    public IEnumerable<string> Post(string str);
}