using Domain.Enums;

namespace Domain.DTOs.EmployeeDTOs;

public class AddEmployeeDto : BaseEmployeeDto
{
    public Position Position { get; set; }
}