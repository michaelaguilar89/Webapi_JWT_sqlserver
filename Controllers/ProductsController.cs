using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;
using WebApi_JWT.Models;
using WebApi_JWT.Repository_s;
using WebApiProduccion.Models;

namespace WebApi_JWT.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProducts _produtcs;
		protected myResponse _response;
		public ProductsController(IProducts products)
		{
			_produtcs = products;
			_response = new myResponse();
		}

		
	

		[HttpGet]
		public async Task<ActionResult> GetAllProducts()
		{
			try
			{
				var resp = new List<Products>();
				resp = await _produtcs.GetProducts();
				if (resp==null)
				{
					_response.DisplayMessage = "Data not found";
					return BadRequest(_response);
				}
				_response.DisplayMessage = "List of Produtcs";
				_response.Result = resp;
				return Ok(_response);

			}
			catch (Exception e)
			{

				_response.ErrorMessages = new List<string> { e.Message };
				_response.DisplayMessage = "Error";
				return BadRequest(_response);

			}
		}
		[HttpGet("{id}")]
		public async Task<ActionResult> getProductById(int id)
		{
			try
			{
				Products myProduct = await _produtcs.GetProductById(id);
				if (myProduct == null)
				{
					_response.DisplayMessage = "Product Id : " + id + " not found";
					return BadRequest(_response);
				}
				_response.DisplayMessage = "Data of : " + id;
				_response.Result = myProduct;
				return Ok(_response);
			}
			catch (Exception e)
			{

				_response.ErrorMessages = new List<string> { e.Message };
				_response.DisplayMessage = "Error";
				return BadRequest(_response);

			}
		}
		[HttpPost]
		public async Task<ActionResult> CreateProduct(Products newProduct)
		{

			try
			{
				var message = await _produtcs.CreateUpdate(newProduct);
				if (message== "insert!")
				{
					_response.DisplayMessage = "insert new Record";
					return Ok(_response);
				}
				_response.DisplayMessage = "internal server error";
				return BadRequest(_response);
			}
			catch (Exception e)
			{

				_response.ErrorMessages = new List<string> { e.Message };
				_response.DisplayMessage = "Error";
				return BadRequest(_response);

			}
		}

		[HttpPut]
		public async Task<ActionResult> UpdateProduct(Products updateProduct)
		{
			try
			{
				var messages = await _produtcs.CreateUpdate(updateProduct);
				if (messages== "update!")
				{
					_response.DisplayMessage = "Update Record!";
					return Ok(_response);
				}
				_response.DisplayMessage = "internal server error";
				return BadRequest(_response);
			}
			catch (Exception e)
			{

				_response.ErrorMessages = new List<string> { e.Message };
				_response.DisplayMessage = "Error";
				return BadRequest(_response);

			}
		}
		[HttpDelete("{id}")]
		public async Task<ActionResult> deleteProduct(int id)
		{
			try
			{
				var message = await _produtcs.Delete(id);
				if (message == "delete!")
				{
					_response.DisplayMessage = "Product has been removed";
					return Ok(_response);
				}
				_response.DisplayMessage = "internal server error";
				return BadRequest(_response);
			}
			catch (Exception e)
			{

				_response.ErrorMessages = new List<string> { e.Message };
				_response.DisplayMessage = "Error";
				return BadRequest(_response);

			}
		}
	}

}
