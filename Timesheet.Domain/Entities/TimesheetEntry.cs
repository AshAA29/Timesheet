namespace Timesheet.Domain.Entities;

public class TimesheetEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = "";
    public string ProjectId { get; set; } = "";
    public DateOnly Date { get; set; }
    public decimal Hours { get; set; }
    public string? Description { get; set; }
}