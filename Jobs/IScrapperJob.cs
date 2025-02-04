namespace dotnet_api.Jobs;

public interface IScrapperJob
{
    public void Run(string[] Stocks);
}