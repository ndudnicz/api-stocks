using dotnet_api.Data.Entities;

namespace dotnet_api.Repositories;

public class ElementRepository(): IElementRepository
{
    private static readonly List<Element> Elements = [
        new Element { Id = 1, Str = "Element 1" },
        new Element { Id = 2, Str = "Element 2" },
        new Element { Id = 3, Str = "Element 3" }
    ];
    private static int lastId = 3;

    public IEnumerable<Element> Get()
    {
        return Elements;
    }
    
    public Element Create(Element element)
    {
        element.Id = lastId + 1;
        lastId++;
        Elements.Add(element);
        return element;
    }
}