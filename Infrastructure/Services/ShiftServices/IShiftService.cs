using Domain.Wrappers;

namespace Infrastructure.Services.ShiftServices;

public interface IShiftService
{
    Task<Response<string>> StartShiftAsync(string employeeId);
    Task<Response<string>> EndShiftAsync(string employeeId);
}