using Domain.Entities;

namespace Domain.DTOs.EmployeeDTOs;

public class GetEmployeeByIdDto : BaseEmployeeDto
{
    public string Id { get; set; }
    public string Position { get; set; }
    public List<GetShiftDto> Shifts { get; set; } = new();
}