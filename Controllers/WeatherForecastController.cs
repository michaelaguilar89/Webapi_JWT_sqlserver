using Microsoft.AspNetCore.Mvc;
using WebApi_JWT.Models;

namespace WebApi_JWT.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

		 List<Products> products = new List<Products>() {
			new Products {Name="Camiseta Unisex",Category="Ropa",Price=3000,Stock=100 },
			new Products {Name="Pantalon Levis",Category="Ropa",Price=15000,Stock=90 },
			new Products {Name="Vestido Dama",Category="Ropa",Price=8000,Stock=800 }
		};

		List<Users> users = new List<Users>() {
		new Users{ UserName="Michael", Password="123456",Rol="Administrador"},
		new Users{ UserName="Abraham", Password="123456",Rol="Supervisor"}
			};

		private readonly ILogger<WeatherForecastController> _logger;
		private readonly IConfiguration _config;
		public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config)
		{
			_logger = logger;
			_config = config;
		}

		[HttpPut(Name ="GetProducts")]
		public IActionResult GetProducts()
		{
			return  Ok(products);
		}

		[HttpPost(Name ="GetUsers")]
		public IActionResult GetUsers()
		{
			return Ok(users);
		}

		[HttpGet(Name = "GetWeatherForecast")]
		public IEnumerable<WeatherForecast> Get()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}
	}
}