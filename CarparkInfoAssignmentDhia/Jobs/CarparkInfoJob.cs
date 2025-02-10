using CarparkInfoAssignmentDhia.CarparkInfo.Dtos;
using CarparkInfoAssignmentDhia.SettingsDtos;
using CsvHelper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Logging;
using System.Globalization;

namespace CarparkInfoAssignmentDhia.Jobs;

public class CarparkInfoJob : IJob
{
    private readonly ILogger<CarparkInfoJob> logger;
    private readonly CarparkInfoJobSettings settings;

    public CarparkInfoJob(ILogger<CarparkInfoJob> logger, IOptions<CarparkInfoJobSettings> settings)
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

            try
            {
                using var reader = new StreamReader(file);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var records = csv.GetRecordsAsync<CsvDto>(context.CancellationToken);

                await foreach (var record in records)
                {
                    logger.LogInformation("Processing record: {Record}", record.car_park_no);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error processing file {file}", file);
            }
        }
    }

    private static string[] LookForFiles(
        ILogger<CarparkInfoJob> logger,
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
