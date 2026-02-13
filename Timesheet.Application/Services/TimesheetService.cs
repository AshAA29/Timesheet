using Timesheet.Application.Interfaces;
using Timesheet.Application.Validation;
using Timesheet.Domain.Entities;
using Timesheet.Domain.Interfaces;

namespace Timesheet.Application.Services;

public class TimesheetService : ITimesheetService
{
    private readonly ITimesheetRepository _repo;

    public TimesheetService(ITimesheetRepository repo)
    {
        _repo = repo;
    }

    public TimesheetEntry Add(TimesheetEntry entry)
    {
        TimesheetValidator.Validate(entry);

        bool duplicate = _repo.GetAll().Any(e =>
            e.UserId == entry.UserId &&
            e.ProjectId == entry.ProjectId &&
            e.Date == entry.Date);

        if (duplicate)
        {
            throw new InvalidOperationException("Duplicate entry for user, project and date.");
        }

        _repo.Add(entry);
        return entry;
    }

    public TimesheetEntry Update(TimesheetEntry entry)
    {
        TimesheetValidator.Validate(entry);

        var existing = _repo.GetById(entry.Id)
            ?? throw new KeyNotFoundException("Entry not found.");

        bool duplicate = _repo.GetAll().Any(e =>
            e.Id != entry.Id &&
            e.UserId == entry.UserId &&
            e.ProjectId == entry.ProjectId &&
            e.Date == entry.Date);

        if (duplicate)
        {
            throw new InvalidOperationException("Duplicate entry for user, project and date.");
        }

        _repo.Update(entry);
        return entry;
    }

    public void Delete(Guid id)
    {
        var existing = _repo.GetById(id) ?? throw new KeyNotFoundException("Entry not found.");

        _repo.Delete(id);
    }

    public IEnumerable<TimesheetEntry> GetEntriesForWeek(string userId, DateOnly weekStart)
    {
        var weekEnd = weekStart.AddDays(7);

        return _repo.GetAll()
            .Where(e => e.UserId == userId &&
                        e.Date >= weekStart &&
                        e.Date < weekEnd)
            .OrderBy(e => e.Date)
            .ThenBy(e => e.ProjectId);
    }

    public decimal GetTotalHours(string userId, string projectId, DateOnly weekStart)
    {
        var weekEnd = weekStart.AddDays(7);

        return _repo.GetAll()
            .Where(e => e.UserId == userId &&
                        e.ProjectId == projectId &&
                        e.Date >= weekStart &&
                        e.Date < weekEnd)
            .Sum(e => e.Hours);
    }
}