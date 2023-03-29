using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using WebApi_JWT.Connection;
using WebApi_JWT.Repository_s;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
	options.AddPolicy(MyAllowSpecificOrigins,
						  policy =>
						  {
							  policy.WithOrigins("*")
												  .AllowAnyHeader()
												  .AllowAnyMethod();
						  });
});
// Add services to the container.

builder.Services.AddDbContext<Context>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUser, User_Repository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(
				System.Text.Encoding.ASCII.GetBytes(
					builder.Configuration.GetSection("appSettings:Token").Value)),
			ValidateIssuer = false,
			ValidateAudience = false
		};
	});
builder.Services.AddSwaggerGen(c =>
{
	c.OperationFilter<SecurityRequirementsOperationFilter>();

	c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
	{
		Description = "Autorizacion Standar, Use Bearer. Example \" bearer {token}\"",
		In = ParameterLocation.Header,
		Name="Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme ="Bearer"
	});
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
