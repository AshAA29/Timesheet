using Timesheet.Domain.Entities;

namespace Timesheet.Application.Validation;

public static class TimesheetValidator
{
    public static void Validate(TimesheetEntry entry)
    {
        if (entry.Hours <= 0)
        {
            throw new ArgumentException("Hours must be greater than zero.", nameof(entry.Hours));
        }

        if (entry.Hours > 9)
        {
            throw new ArgumentException("Hours cannot exceed 9 per day.", nameof(entry.Hours));
        }
    }
}