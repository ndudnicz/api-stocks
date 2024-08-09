namespace dotnet_api.Services;

public interface IHomeService
{
    public IEnumerable<string> Get();
    public IEnumerable<string> Post(string str);
}