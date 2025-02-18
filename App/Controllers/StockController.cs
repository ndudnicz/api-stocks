using dotnet_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers;

[Route("v1/[controller]")]
[ApiController]
public class StockController(IStockService stockService) : ControllerBase
{
    private readonly IStockService _stockService = stockService ?? throw new ArgumentNullException(nameof(stockService));
    
    [HttpGet("")]
    public IActionResult Get()
    {
        return Ok(_stockService.Get());
    }
}