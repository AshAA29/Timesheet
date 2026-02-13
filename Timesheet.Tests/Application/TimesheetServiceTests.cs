using Timesheet.Application.Services;
using Timesheet.Domain.Entities;
using Timesheet.Infrastructure.Persistence;

namespace Timesheet.Tests.Application;

[TestFixture]
public class TimesheetServiceTests
{
    private TimesheetService _service;

    [SetUp]
    public void Setup()
    {
        var repo = new TimesheetRepository();
        _service = new TimesheetService(repo);
    }

    [Test]
    public void Add_Throws_ArgumentException_When_HoursAreZero()
    {
        var entry = new TimesheetEntry
        {
            UserId = "A1",
            ProjectId = "B1",
            Date = new DateOnly(2025, 1, 1),
            Hours = 0m
        };

        Assert.Throws<ArgumentException>(() => _service.Add(entry));
    }

    [Test]
    public void Add_Throws_ArgumentException_When_HoursExceed9()
    {
        var entry = new TimesheetEntry
        {
            UserId = "A1",
            ProjectId = "B1",
            Date = new DateOnly(2025, 1, 1),
            Hours = 9.5m
        };

        Assert.Throws<ArgumentException>(() => _service.Add(entry));
    }

    [Test]
    public void Add_Throws_InvalidOperationException_OnDuplicateEntry()
    {
        var date = new DateOnly(2025, 1, 1);

        _service.Add(new TimesheetEntry
        {
            UserId = "A1",
            ProjectId = "B1",
            Date = date,
            Hours = 7m
        });

        var duplicate = new TimesheetEntry
        {
            UserId = "A1",
            ProjectId = "B1",
            Date = date,
            Hours = 5m
        };

        Assert.Throws<InvalidOperationException>(() => _service.Add(duplicate));
    }

    [Test]
    public void GetTotalHours_Returns_CorrectSum()
    {
        var weekStart = new DateOnly(2025, 1, 6);

        _service.Add(new TimesheetEntry
        {
            UserId = "A1",
            ProjectId = "B1",
            Date = weekStart,
            Hours = 4m
        });

        _service.Add(new TimesheetEntry
        {
            UserId = "A1",
            ProjectId = "B1",
            Date = weekStart.AddDays(1),
            Hours = 3.5m
        });

        var total = _service.GetTotalHours("A1", "B1", weekStart);

        Assert.That(total, Is.EqualTo(7.5m));
    }

    [Test]
    public void Update_Throws_KeyNotFoundException_When_EntryNotFound()
    {
        var entry = new TimesheetEntry
        {
            Id = Guid.NewGuid(),
            UserId = "A1",
            ProjectId = "B1",
            Date = new DateOnly(2025, 1, 1),
            Hours = 5m
        };

        Assert.Throws<KeyNotFoundException>(() => _service.Update(entry));
    }

    [Test]
    public void Delete_Throws_KeyNotFoundException_When_EntryNotFound()
    {
        Assert.Throws<KeyNotFoundException>(() => _service.Delete(Guid.NewGuid()));
    }
}