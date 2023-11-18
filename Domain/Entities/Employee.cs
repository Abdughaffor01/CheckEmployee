using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class Employee
{
    [Key]
    public string Id { get; set; }

    [MaxLength(40)]
    public string LastName { get; set; }
    
    [MaxLength(40)]
    public string FirtName { get; set; }
    
    [MaxLength(40)]
    public string FatherName { get; set; }
    
    public Position Position { get; set; }
    
    public List<Shift> Shifts { get; set; }
}