namespace dotnet_api.Data.Entities;

public struct Stock
{
    public string Name { get; set; }
    public string Isin { get; set; }
    public string Exchange { get; set; }
    public double Variation { get; set; }
    public double LastPrice { get; set; }
}