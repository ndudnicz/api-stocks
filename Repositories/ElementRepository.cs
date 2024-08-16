using dotnet_api.Data.Entities;

namespace dotnet_api.Repositories;

public class ElementRepository(): IElementRepository
{
    private static readonly List<Element> Elements = [
        new Element { Str = "Element 1" },
        new Element { Str = "Element 2" },
        new Element { Str = "Element 3" }
    ];

    public IEnumerable<Element> Get()
    {
        return Elements;
    }
    
    public Element Create(Element element)
    {
        Elements.Add(element);
        return element;
    }
}