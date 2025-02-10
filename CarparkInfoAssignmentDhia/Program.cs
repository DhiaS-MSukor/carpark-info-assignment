using CarparkInfoAssignmentDhia.Jobs;
using CarparkInfoAssignmentDhia.SettingsDtos;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CarparkInfoJobSettings>(builder.Configuration.GetSection("CsvSettings"));

builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("CarparkJob");

    q.AddJob<CarparkInfoJob>(opts => opts.WithIdentity(jobKey));
    //q.UsePersistentStore(s => s.UseMicrosoftSQLite(""));

    q.AddTrigger(opts => opts
         .ForJob(jobKey)
         .WithIdentity("CarparkJob-trigger")
         .WithCronSchedule("* * * * * ?")); // Cron: sec, min, hour, day, month, day-of-week.
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
