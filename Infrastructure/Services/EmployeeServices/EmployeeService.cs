using System.Net;
using Domain.DTOs;
using Domain.DTOs.EmployeeDTOs;
using Domain.Entities;
using Domain.Wrappers;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.EmployeeServices;

public class EmployeeService : IEmployeeService
{
    private readonly DataContext _context;
    public EmployeeService(DataContext context) => _context = context;


    public async Task<Response<List<GetEmployeeDto>>> GetEmployeesAsync()
    {
        var employees = await _context.Employees.Select(e => new GetEmployeeDto()
        {
            Id = e.Id,
            FirtName = e.FirtName,
            LastName = e.LastName,
            FatherName = e.FatherName,
            Position = e.Position.ToString()
        }).ToListAsync();
        if (employees.Count == 0) return new Response<List<GetEmployeeDto>>(HttpStatusCode.NoContent);
        return new Response<List<GetEmployeeDto>>(employees);
    }

    public async Task<Response<GetEmployeeByIdDto>> GetEmployeeByIdAsync(string id)
    {
        try
        {
            var employee =await (from e in _context.Employees
                where e.Id == id
                select new GetEmployeeByIdDto()
                {
                    Id = e.Id,
                    FirtName = e.FirtName,
                    LastName = e.LastName,
                    FatherName = e.FatherName,
                    Position = e.Position.ToString(),
                    Shifts = (from s in e.Shifts
                        where s.EmployeeId == e.Id
                        select new GetShiftDto()
                        {
                            Id = s.Id,
                            EndShift = s.EndShift,
                            StartShift = s.StartShift,
                            NumOfHoursWorked = s.NumOfHoursWorked
                        }).ToList()
                }).FirstOrDefaultAsync();
     
        if (employee == null) return new Response<GetEmployeeByIdDto>(HttpStatusCode.BadRequest);
        return new Response<GetEmployeeByIdDto>(employee);
    }
    catch (Exception ex)
    {
        return new Response<GetEmployeeByIdDto>(HttpStatusCode.InternalServerError, ex.Message);
    }
}

public async Task<Response<bool>> AddEmployeeAsync(AddEmployeeDto model)
{
    try
    {
        var employee = new Employee()
        {
            Id = Guid.NewGuid().ToString(),
            FirtName = model.FirtName,
            LastName = model.LastName,
            FatherName = model.FatherName,
            Position = model.Position,
        };
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
        return new Response<bool>(true);
    }
    catch (Exception ex)
    {
        return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
    }
}

public async Task<Response<bool>> UpdateEmployeeAsync(UpdateEmployeeDto model)
{
    try
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == model.Id);
        if (employee == null) return new Response<bool>(HttpStatusCode.BadRequest);
        employee.Position = model.Position;
        employee.FirtName = model.FirtName;
        employee.LastName = model.LastName;
        employee.FatherName = model.FatherName;
        await _context.SaveChangesAsync();
        return new Response<bool>(true);
    }
    catch (Exception ex)
    {
        return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
    }
}

public async Task<Response<bool>> DeleteEmployeeAsync(string id)
{
    try
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        if (employee == null) return new Response<bool>(HttpStatusCode.BadRequest);
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return new Response<bool>(true);
    }
    catch (Exception ex)
    {
        return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
    }
}

}