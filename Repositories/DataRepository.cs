namespace dotnet_api.Repositories;

public class DataRepository(): IDataRepository
{
    private static readonly List<string> Data = ["1", "2"];

    public IEnumerable<string> Get()
    {
        return Data;
    }
    
    public IEnumerable<string> Post(string str)
    {
        Data.Add(str);
        return Data;
    }
}