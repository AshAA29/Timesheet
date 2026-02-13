using Microsoft.Extensions.DependencyInjection;
using Timesheet.Application.Interfaces;
using Timesheet.Application.Services;
using Timesheet.Domain.Interfaces;
using Timesheet.Infrastructure.Persistence;

namespace Timesheet.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTimesheet(this IServiceCollection services)
    {
        services.AddSingleton<ITimesheetRepository, TimesheetRepository>();
        services.AddScoped<ITimesheetService, TimesheetService>();

        return services;
    }
}