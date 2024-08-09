using dotnet_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers;

public class PostDto
{
    public string Str { get; set; }
}

[Route("v1/[controller]")]
[ApiController]
public class HomeController(IHomeService homeService) : ControllerBase
{
    private readonly IHomeService _homeService = homeService ?? throw new ArgumentNullException(nameof(homeService));
    [HttpGet("")]
    public IActionResult Get()
    {
        return Ok(_homeService.Get());
    }
    
    [HttpPost("")]
    public IActionResult Post([FromBody] PostDto dto)
    {
        return Ok(_homeService.Post(dto.Str));
    }
}