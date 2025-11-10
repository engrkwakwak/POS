using Hangfire;
using POS.Infrastructure.Outbox;

namespace POS.Api.Extensions;

internal static class BackgroundJobExtensions
{
    public static IApplicationBuilder UseBackgroundJobs(this WebApplication app)
    {
        IRecurringJobManager recurringJobManager = app.Services.GetRequiredService<IRecurringJobManager>();

        string? schedule = app.Configuration["BackgroundJobs:Outbox:Schedule"];
        if (string.IsNullOrWhiteSpace(schedule))
        {
            throw new InvalidOperationException("Configuration key 'BackgroundJobs:Outbox:Schedule' is required.");
        }

        recurringJobManager.AddOrUpdate<IProcessOutboxMessagesJob>(
            "outbox-processor",
            job => job.ProcessAsync(),
            schedule);

        return app;
    }
}
