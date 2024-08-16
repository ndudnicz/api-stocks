using dotnet_api.Entities.Dtos;
using dotnet_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers;

[Route("v1/[controller]")]
[ApiController]
public class ElementController(IElementService elementService) : ControllerBase
{
    private readonly IElementService _elementService = elementService ?? throw new ArgumentNullException(nameof(elementService));
    [HttpGet("")]
    public IActionResult Get()
    {
        return Ok(_elementService.Get());
    }
    
    [HttpPost("")]
    public IActionResult Create([FromBody] CreateElementDto dto)
    {
        return Ok(_elementService.Create(dto));
    }
}