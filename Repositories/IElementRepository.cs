using dotnet_api.Data.Entities;

namespace dotnet_api.Repositories;

public interface IElementRepository
{
    public IEnumerable<Element> Get();
    public Element Create(Element element);
}