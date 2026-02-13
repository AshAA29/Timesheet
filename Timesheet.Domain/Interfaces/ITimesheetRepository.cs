using Timesheet.Domain.Entities;

namespace Timesheet.Domain.Interfaces;

public interface ITimesheetRepository
{
    void Add(TimesheetEntry entry);
    void Update(TimesheetEntry entry);
    void Delete(Guid id);
    TimesheetEntry? GetById(Guid id);
    IEnumerable<TimesheetEntry> GetAll();
}