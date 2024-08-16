using dotnet_api.Data.Entities;
using dotnet_api.Entities.Dtos;

namespace dotnet_api.Services;

public interface IElementService
{
    public IEnumerable<Element> Get();
    public Element Create(CreateElementDto dto);
}