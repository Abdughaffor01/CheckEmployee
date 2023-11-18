using Domain.DTOs.EmployeeDTOs;
using Domain.Wrappers;

namespace Infrastructure.Services.EmployeeServices;

public interface IEmployeeService
{
    Task<Response<List<GetEmployeeDto>>> GetEmployeesAsync();
    Task<Response<GetEmployeeByIdDto>> GetEmployeeByIdAsync(string id);
    Task<Response<GetEmployeeDto>> AddEmployeeAsync(AddEmployeeDto model);
    Task<Response<bool>> UpdateEmployeeAsync(UpdateEmployeeDto model);
    Task<Response<bool>> DeleteEmployeeAsync(string id);
}