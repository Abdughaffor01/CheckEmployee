namespace Domain.DTOs;

public class GetShiftDto
{
    public int Id { get; set; }

    public DateTime StartShift { get; set; }
    public DateTime? EndShift { get; set; }

    public int NumOfHoursWorked { get; set; }
}