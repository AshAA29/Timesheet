using Timesheet.Domain.Entities;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Infrastructure.Persistence;

public class TimesheetRepository : ITimesheetRepository
{
    private readonly List<TimesheetEntry> _entries = new();

    public void Add(TimesheetEntry entry)
    {
        _entries.Add(entry);
    }

    public void Update(TimesheetEntry entry)
    {
        var index = _entries.FindIndex(e => e.Id == entry.Id);
        if (index >= 0)
        {
            _entries[index] = entry;
        }
    }

    public void Delete(Guid id)
    {
        var entry = _entries.FirstOrDefault(e => e.Id == id);
        if (entry != null)
        {
            _entries.Remove(entry);
        }
    }

    public TimesheetEntry? GetById(Guid id)
    {
        return _entries.FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<TimesheetEntry> GetAll()
    {
        return _entries;
    }
}