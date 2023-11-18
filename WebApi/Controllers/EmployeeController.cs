using Domain.DTOs.EmployeeDTOs;
using Infrastructure.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;
    public EmployeeController(IEmployeeService service)=>_service = service;

    [HttpGet("GetEmployeesAsync")]
    public async Task<IActionResult> GetEmployeesAsync()
    {
        var res = await _service.GetEmployeesAsync();
        return StatusCode(res.StatusCode, res);
    }   
    
    [HttpGet("GetEmployeeByIdAsync")]
    public async Task<IActionResult> GetEmployeeByIdAsync(string id)
    {
        var res = await _service.GetEmployeeByIdAsync(id);
        return StatusCode(res.StatusCode, res);
    } 
    
    [HttpPost("AddEmployeeAsync")]
    public async Task<IActionResult> AddEmployeeAsync(AddEmployeeDto model)
    {
        var res = await _service.AddEmployeeAsync(model);
        return StatusCode(res.StatusCode, res);
    } 
    
    [HttpPut("UpdateEmployeeAsync")]
    public async Task<IActionResult> UpdateEmployeeAsync(UpdateEmployeeDto model)
    {
        var res = await _service.UpdateEmployeeAsync(model);
        return StatusCode(res.StatusCode, res);
    } 
    
    [HttpDelete("DeleteEmployeeAsync")]
    public async Task<IActionResult> DeleteEmployeeAsync(string id)
    {
        var res = await _service.DeleteEmployeeAsync(id);
        return StatusCode(res.StatusCode, res);
    } 
}