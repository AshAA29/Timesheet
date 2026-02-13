using Timesheet.Domain.Entities;
using Timesheet.Infrastructure.Persistence;

namespace Timesheet.Tests.Infrastructure;

[TestFixture]
public class TimesheetRepositoryTests
{
    private TimesheetRepository _repo;

    [SetUp]
    public void Setup()
    {
        _repo = new TimesheetRepository();
    }

    [Test]
    public void Add_AddsEntry()
    {
        var entry = new TimesheetEntry { Hours = 5m };

        _repo.Add(entry);

        Assert.That(_repo.GetAll().Count(), Is.EqualTo(1));
    }

    [Test]
    public void Update_Updates_ExistingEntry()
    {
        var entry = new TimesheetEntry { Hours = 5m };
        _repo.Add(entry);

        entry.Hours = 8m;
        _repo.Update(entry);

        var updated = _repo.GetById(entry.Id);
        Assert.That(updated.Hours, Is.EqualTo(8m));
    }

    [Test]
    public void Update_DoesNothing_When_EntryNotFound()
    {
        var entry = new TimesheetEntry { Hours = 5m };

        _repo.Update(entry);

        Assert.That(_repo.GetAll().Count(), Is.EqualTo(0));
    }

    [Test]
    public void Delete_Removes_Entry()
    {
        var entry = new TimesheetEntry();
        _repo.Add(entry);

        _repo.Delete(entry.Id);

        Assert.That(_repo.GetAll().Count(), Is.EqualTo(0));
    }

    [Test]
    public void Delete_DoesNothing_When_EntryNotFound()
    {
        _repo.Delete(Guid.NewGuid());

        Assert.That(_repo.GetAll().Count(), Is.EqualTo(0));
    }

    [Test]
    public void GetById_Returns_Entry()
    {
        var entry = new TimesheetEntry();
        _repo.Add(entry);

        var result = _repo.GetById(entry.Id);

        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void GetById_ReturnsNull_When_NotFound()
    {
        var result = _repo.GetById(Guid.NewGuid());

        Assert.That(result, Is.Null);
    }
}