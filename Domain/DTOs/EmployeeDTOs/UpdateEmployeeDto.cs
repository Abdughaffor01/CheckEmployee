using Domain.Enums;

namespace Domain.DTOs.EmployeeDTOs;

public class UpdateEmployeeDto : BaseEmployeeDto
{
    public string Id { get; set; }
    public Position Position { get; set; }
}