namespace dotnet_api.Data.Entities;

public class Element
{
    public int Id { get; set; }
    public string Str { get; set; }
    public ElementStatus Status { get; set; }
}

public enum ElementStatus
{
    Todo = 1,
    Doing = 2,
    Done = 3
}