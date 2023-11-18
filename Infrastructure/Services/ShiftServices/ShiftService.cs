using System.Net;
using Domain.Entities;
using Domain.Wrappers;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ShiftServices;

public class ShiftService : IShiftService
{
    private readonly DataContext _context;
    public ShiftService(DataContext context) => _context = context;

    public async Task<Response<string>> StartShiftAsync(string employeeId)
    {
        try
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
            if (employee == null) return new Response<string>(HttpStatusCode.BadRequest);
            var startShiftCheck = await _context.Shifts
                .FirstOrDefaultAsync(s =>
                    s.EmployeeId == employeeId &
                    s.StartShift.ToShortDateString() == DateTime.UtcNow.ToShortDateString());

            if (startShiftCheck != null) return new Response<string>(HttpStatusCode.BadRequest);

            var startShift = new Shift()
            {
                EmployeeId = employeeId,
                StartShift = DateTime.UtcNow,
                EndShift = DateTime.UtcNow.AddHours(9)
            };
            await _context.Shifts.AddAsync(startShift);
            await _context.SaveChangesAsync();
            return new Response<string>("Successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> EndShiftAsync(string employeeId)
    {
        try
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
            if (employee == null) return new Response<string>(HttpStatusCode.BadRequest);
            var shiftCheck = await _context.Shifts
                .FirstOrDefaultAsync(e =>
                    e.EmployeeId == employeeId &
                    e.StartShift.ToShortDateString() == DateTime.UtcNow.ToShortDateString());

            if (DateTime.UtcNow.Hour - shiftCheck!.StartShift.Hour < 4)
                return new Response<string>(HttpStatusCode.BadRequest, "Although you did not work 70% of your work");

            shiftCheck.EndShift = DateTime.UtcNow;
            shiftCheck.NumOfHoursWorked = shiftCheck.StartShift.AddHours(9).Hour - DateTime.UtcNow.Hour;

            await _context.SaveChangesAsync();
            return new Response<string>("Successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}