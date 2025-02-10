using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Logging;

namespace CarparkInfoAssignmentDhia.Jobs.Carpark;

public class CarparkJob : IJob
{
    private readonly ILogger<CarparkJob> logger;
    private readonly CarparkJobSettings settings;

    public CarparkJob(ILogger<CarparkJob> logger, IOptions<CarparkJobSettings> settings)
    {
        this.logger = logger;
        this.settings = settings.Value;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var files = LookForFiles(logger, settings.BaseFilePath, settings.DateSuffixFormat, settings.FilePrefix, settings.FileExtension);
        foreach (var file in files)
        {
            var fullPath = Path.GetFullPath(file);
            logger.LogInformation("Processing file: {file}", fullPath);
        }
    }

    private static string[] LookForFiles(
        ILogger<CarparkJob> logger,
        string? baseFilePath,
        string? dateSuffixFormat,
        string? filePrefix,
        string? fileExtension
        )
    {
        if (!Directory.Exists(baseFilePath))
        {
            logger.LogWarning("CSV directory not found: {BaseFilePath}", baseFilePath);
            return Array.Empty<string>();
        }

        var currentDateSuffix = DateTime.UtcNow.ToString(dateSuffixFormat);
        var searchPattern = $"{filePrefix}{currentDateSuffix}*{fileExtension}";
        return Directory.GetFiles(baseFilePath, searchPattern);
    }

}
