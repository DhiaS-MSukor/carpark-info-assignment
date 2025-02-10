using CarparkInfoAssignmentDhia.CarparkInfo;
using CarparkInfoAssignmentDhia.Jobs;
using CarparkInfoAssignmentDhia.SettingsDtos;
using Microsoft.EntityFrameworkCore;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CarparkInfoJobSettings>(builder.Configuration.GetSection("CsvSettings"));

builder.Services.AddPooledDbContextFactory<CarparkContext>(c => c.UseSqlite("Data Source=:memory:;"));

builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("CarparkJob");

    q.AddJob<CarparkInfoJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
         .ForJob(jobKey)
         .WithIdentity("CarparkJob-trigger")
         .WithCronSchedule("0 0 1 * * ?")); // Cron: sec, min, hour, day, month, day-of-week.

    q.AddTrigger(opts => opts
         .ForJob(jobKey)
         .WithIdentity("CarparkJob-trigger-now")
         .StartNow());
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
