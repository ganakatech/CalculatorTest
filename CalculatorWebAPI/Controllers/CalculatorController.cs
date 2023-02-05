using System.ComponentModel.DataAnnotations;
using CalculatorWebAPI.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CalculatorController : ControllerBase
{
    private readonly ILogger<CalculatorController> _logger;

    public CalculatorController(ILogger<CalculatorController> logger)
    {
        _logger = logger;
    }

    //Can be done using query params with HttpGet as well
    [HttpPost]
    [Route("add")]
    public IActionResult Add([FromBody] AddParams addParams)
    {
        try
        {
            int result = addParams.Start + addParams.Amount;
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error performing add calculation");
            return StatusCode(500,"An unexpected error occurred");
        }
    }
    
    [HttpGet]
    [Route("subtract")]
    public IActionResult Subtract([FromQuery]int start, [FromQuery]int amount)
    {
        try
        {
            int result = start - amount;
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error performing subtract calculation");
            return StatusCode(500,"An unexpected error occurred");
        }
    }
    
    //Can be done using query params with HttpGet as well
    [HttpPost]
    [Route("multiply")]
    public IActionResult Multiply([FromBody] MuliplyParams muliplyParams)
    {
        try
        {
            int result = muliplyParams.Start * muliplyParams.By;
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error performing multiply calculation");
            return StatusCode(500,"An unexpected error occurred");
        }
    }
    
    [HttpGet]
    [Route("divide")]
    public IActionResult Divide([FromQuery][Required] int start, [FromQuery][Required] int by)
    {
        if (by == 0)
        {
            _logger.LogError("Cannot divide by zero");
            return BadRequest("Cannot divide by zero");
        }

        try
        {
            int result = start / by;
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error performing divide calculation");
            return StatusCode(500,"An unexpected error occurred");
        }
    }
}