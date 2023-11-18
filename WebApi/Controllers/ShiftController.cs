using Infrastructure.Services.ShiftServices;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ShiftController : ControllerBase
{
    private readonly IShiftService _service;
    public ShiftController(IShiftService service)=>_service = service;

    [HttpPost("StartShiftAsync")]
    public async Task<IActionResult> StartShiftAsync(string employeeId)
    {
        var res = await _service.StartShiftAsync(employeeId);
        return StatusCode(res.StatusCode, res);
    }
    
    [HttpPut("EndShiftAsync")]
    public async Task<IActionResult> EndShiftAsync(string employeeId)
    {
        var res = await _service.EndShiftAsync(employeeId);
        return StatusCode(res.StatusCode, res);
    }
}