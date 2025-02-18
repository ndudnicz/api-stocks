namespace dotnet_api.Jobs;

public interface IScrapperJob
{
    public Task Run(string[] Stocks);
}