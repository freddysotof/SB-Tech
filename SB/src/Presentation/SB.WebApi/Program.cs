using Microsoft.OpenApi.Models;
using SB.Infrastructure.Extensions;
using SB.WebApi.Extensions;
using SB.WebApi;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SB.WebApi.Configurations;
using SB.Application;
using SB.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplication();
builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddInfrastructureServiceRegistration();

// Add services to the container.
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

});


builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.ConfigureApiControllers(false);

builder.Services.ConfigureCustomModelStateValidation();
builder.Services.AddResponseWrapper();
builder.Services.ConfigureLoggerService(builder.Configuration, builder.Logging);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(opts =>
    {

        List<string> origins = new();
        origins.AddRange(builder.Configuration.GetSection("AllowedOrigins")
            .Get<List<string>>());
        if (builder.Environment.IsProduction())
            opts
          .WithOrigins(origins.ToArray())
          .AllowAnyMethod()
          .AllowAnyHeader()
          .SetIsOriginAllowedToAllowWildcardSubdomains();
        else
            opts
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                  .SetIsOriginAllowedToAllowWildcardSubdomains();
    });
});

builder.Services.AddAuthentication()
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:JwtSecret"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

var middlewareSettings = builder.Configuration.GetSection("MiddlewareSettings").Get<MiddlewareSettings>();
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();

app.UseSwaggerUI(c => c.DocumentTitle = $"{(builder.Environment.IsProduction() ? "" : builder.Environment.EnvironmentName)} SB Api");


if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();


app.UseRouting();

app.UseCors();

app.UseAuthorization();
app.UseAuthentication();


app.UseGlobalErrorHandlerMiddleware();

app.UseLoggingMiddleware();


if (middlewareSettings.UsePaginationResponseWrapperMiddleware)
    app.UsePaginationResponseWrapperMiddleware();
else
    app.UseResponseWrapperMiddleware();

app.UseModelStateValidationMiddleware();


app.MapControllers();

app.Run();

