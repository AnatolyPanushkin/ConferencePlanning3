using System.Net;
using ConferencePlanning.Data;
using ConferencePlanning.Data.Entities;
using ConferencePlanning.IdentityServices;
using ConferencePlanning.Services.ConferenceServices;
using ConferencePlanning.Services.PhotosServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ConferencePlanningContext>(options=>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConferencePlanningContext")));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy",policy =>
    {
        policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
    });
});

builder.Services.AddScoped<IConferenceService, ConferenceService>();
builder.Services.AddScoped<IPhotosService, PhotosService>();

builder.Services.AddIdentityService(builder.Configuration);


var app = builder.Build();

//add new users into db
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var userManager = services.GetRequiredService<UserManager<User>>();
await Seed.SeedData(userManager,services);
//------------------------------//


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();