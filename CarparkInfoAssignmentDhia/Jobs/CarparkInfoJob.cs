using CarparkInfoAssignmentDhia.CarparkInfo;
using CarparkInfoAssignmentDhia.CarparkInfo.Dtos;
using CarparkInfoAssignmentDhia.CarparkInfo.Entities;
using CarparkInfoAssignmentDhia.CarparkInfo.Exts;
using CarparkInfoAssignmentDhia.CarparkInfo.Queries;
using CarparkInfoAssignmentDhia.SettingsDtos;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Logging;
using System.Globalization;

namespace CarparkInfoAssignmentDhia.Jobs;

public class CarparkInfoJob : IJob
{
    private readonly ILogger<CarparkInfoJob> logger;
    private readonly IDbContextFactory<CarparkContext> contextFactory;
    private readonly CarparkInfoJobSettings settings;

    public CarparkInfoJob(
        ILogger<CarparkInfoJob> logger,
        IOptions<CarparkInfoJobSettings> settings,
        IDbContextFactory<CarparkContext> contextFactory)
    {
        this.logger = logger;
        this.contextFactory = contextFactory;
        this.settings = settings.Value;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var files = LookForFiles(logger, settings.BaseFilePath, settings.DateSuffixFormat, settings.FilePrefix, settings.FileExtension);
        foreach (var file in files)
        {
            var fullPath = Path.GetFullPath(file);
            logger.LogInformation("Processing file: {file}", fullPath);

            using var dbContext = await contextFactory.CreateDbContextAsync(context.CancellationToken);
            using var transaction = dbContext.Database.BeginTransaction();

            try
            {
                using var reader = new StreamReader(file);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var records = csv.GetRecordsAsync<CsvDto>(context.CancellationToken);

                await foreach (var record in records)
                {
                    logger.LogInformation("Processing record: {Record}", record.car_park_no);

                    var carparkType = await new CarParkTypeGetByName(record.car_park_type)
                        .Query(dbContext.CarParkTypes)
                        .LastOrDefaultAsync();
                    var freeParkingType = await new FreeParkingTypeGetByName(record.free_parking)
                        .Query(dbContext.FreeParkingTypes)
                        .LastOrDefaultAsync();
                    var parkingSystemType = await new ParkingSystemTypeGetByName(record.type_of_parking_system)
                        .Query(dbContext.ParkingSystemTypes)
                        .LastOrDefaultAsync();
                    var shortTermParkingType = await new ShortTermParkingTypeGetByName(record.short_term_parking)
                        .Query(dbContext.ShortTermParkingTypes)
                        .LastOrDefaultAsync();

                    var existing = await new CarParkGetByNo(record.car_park_no)
                        .Query(dbContext.Carparks)
                        .FirstOrDefaultAsync();
                    if (existing != null)
                    {
                        existing.MergeRecord(record);
                        existing.CarParkType = carparkType;
                        existing.FreeParkingType = freeParkingType;
                        existing.ParkingSystemType = parkingSystemType;
                        existing.ShortTermParkingType = shortTermParkingType;

                        dbContext.Update(existing);
                    }
                    else
                    {
                        var newCarpark = record.ToCarpark();
                        newCarpark.CarParkType = carparkType;
                        newCarpark.FreeParkingType = freeParkingType;
                        newCarpark.ParkingSystemType = parkingSystemType;
                        newCarpark.ShortTermParkingType = shortTermParkingType;

                        await dbContext.AddAsync(newCarpark);
                    }
                }

                await transaction.CommitAsync(context.CancellationToken);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error processing file {file}", file);
                await transaction.RollbackAsync(CancellationToken.None);
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
