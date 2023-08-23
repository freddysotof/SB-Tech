using Microsoft.OpenApi.Models;
using SB.WebApi.Extensions;
using SB.WebApi;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SB.Application;
using SB.Persistence;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);

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
builder.Services.AddEncryptionValidator();
builder.Services.ConfigureLoggerService(builder.Configuration, builder.Logging);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(opts =>
    {

        List<string> origins = new();
        origins.AddRange(builder.Configuration.GetSection("AllowedOrigins")
            .Get<List<string>>());
            opts
          .WithOrigins(origins.ToArray())
          .AllowAnyMethod()
          .AllowAnyHeader()
          .SetIsOriginAllowedToAllowWildcardSubdomains();
     
    });
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
//Adding Jwt Bearer
.AddJwtBearer("Local",options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    var validAudience = builder.Configuration["JWT:ValidAudience"];
    var validIssuer = builder.Configuration["JWT:ValidIssuer"];
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = !string.IsNullOrEmpty(validIssuer),
        ValidateAudience = !string.IsNullOrEmpty(validAudience),
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,

        ValidAudience = !string.IsNullOrEmpty(validAudience) ? validAudience : "*",
        ValidIssuer = !string.IsNullOrEmpty(validIssuer) ? validIssuer : "*",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
})
 .AddJwtBearer("Firebase",options =>
 {
     options.Authority = "https://securetoken.google.com/sb-tech-test";
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidIssuer = "https://securetoken.google.com/sb-tech-test",
         ValidateAudience = true,
         ValidAudience = "sb-tech-test",
         ValidateLifetime = true
     };
 });

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .AddAuthenticationSchemes("Firebase", "Local")
    .Build();
});

var middlewareSettings = builder.Configuration.GetSection("MiddlewareSettings").Get<MiddlewareSettings>();

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


app.UseAuthentication();

app.UseAuthorization();



app.UseGlobalErrorHandlerMiddleware();

app.UseLoggingMiddleware();

app.UseHashAuthorizationValidator();

if (middlewareSettings.UsePaginationResponseWrapperMiddleware)
    app.UsePaginationResponseWrapperMiddleware();
else
    app.UseResponseWrapperMiddleware();

app.UseModelStateValidationMiddleware();


app.MapControllers();

app.Run();

