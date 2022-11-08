using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Tabkhity.Core.Identity;
using Tabkhity.Errors;
using Tabkhity.Extensions;
using Tabkhity.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Configure Services

builder.Services.AddDbContext<TabhkityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<TabhkityDbContext>();

builder.Services.AddControllers();

// override ApiController attribute behavior to return custom error
// instead of default error of modelState
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
        .SelectMany(e => e.Value.Errors)
        .Select(e => e.ErrorMessage)
        .ToArray();

        return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = errors });
    };
});

builder.Services.AddApplicationServices();
builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerSettings();

var app = builder.Build();

#endregion

#region Configure

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion