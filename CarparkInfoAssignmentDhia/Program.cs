using CarparkInfoAssignmentDhia.CarparkInfo;
using CarparkInfoAssignmentDhia.Dtos;
using CarparkInfoAssignmentDhia.Jobs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/user/login";
        options.LogoutPath = "/user/logout";
    });
builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<CarparkInfoJobSettings>(builder.Configuration.GetSection("CarparkInfoJobSettings"));

builder.Services.AddPooledDbContextFactory<CarparkContext>(c =>
{
    var connectionString = builder.Configuration.GetConnectionString("CarparkInfoDatabase")!;
    c.UseSqlite(connectionString);
});

builder.Services.AddQuartz(q =>
{
    q.UsePersistentStore(s =>
    {
        var connectionString = builder.Configuration.GetConnectionString("QuartzDatabase")!;
        s.UseMicrosoftSQLite(connectionString);
        s.UseNewtonsoftJsonSerializer();
    });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
