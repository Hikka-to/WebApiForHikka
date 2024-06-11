using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using WebApiForHikka.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        [new()
        {
            Reference = new()
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        }] = []
    });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddLoggingMiddleware();
builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));

builder.Services.AddIdentity();
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthentication(); // Use authentication middleware
app.UseAuthorization(); // Use authorization middleware
app.UseLoggingMiddleware();
app.MapControllers();

app.Run();