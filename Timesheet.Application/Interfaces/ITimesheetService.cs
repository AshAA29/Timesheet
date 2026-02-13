using Timesheet.Domain.Entities;

namespace Timesheet.Application.Interfaces;

public interface ITimesheetService
{
    TimesheetEntry Add(TimesheetEntry entry);
    TimesheetEntry Update(TimesheetEntry entry);
    void Delete(Guid id);
    IEnumerable<TimesheetEntry> GetEntriesForWeek(string userId, DateOnly weekStart);
    decimal GetTotalHours(string userId, string projectId, DateOnly weekStart);
}