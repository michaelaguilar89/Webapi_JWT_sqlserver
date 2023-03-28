using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;
using WebApi_JWT.Models;

namespace WebApi_JWT.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};
		string[] names = new[]
		{
			"juan","pablo","stefanny"
		};
	

		[HttpGet("getProducts")]
		public ActionResult getProducts()
		{
			return Ok(Summaries);
		}

		[Authorize]
		[HttpGet("getUsers")]
		public ActionResult getUsers()
		{
			return Ok(names);
		}

	}
}
