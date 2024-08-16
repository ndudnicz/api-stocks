using dotnet_api.Data.Entities;
using dotnet_api.Entities.Dtos;
using dotnet_api.Repositories;

namespace dotnet_api.Services;

public class ElementService(IElementRepository elementRepository): IElementService
{
    private readonly IElementRepository _elementRepository = elementRepository ?? throw new ArgumentNullException(nameof(elementRepository));
    
    public IEnumerable<Element> Get()
    {
        return _elementRepository.Get();
    }
    
    public Element Create(CreateElementDto dto)
    {
        return _elementRepository.Create(new Element
        {
            Str = dto.Str
        });
    }
}