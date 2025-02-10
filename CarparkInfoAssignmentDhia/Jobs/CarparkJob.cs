using Quartz;

namespace CarparkInfoAssignmentDhia.Jobs;

public class CarparkJob : IJob
{
    private readonly ILogger<CarparkJob> logger;

    public CarparkJob(ILogger<CarparkJob> logger)
    {
        this.logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("test");
    }
}
