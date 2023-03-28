﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_JWT.Models;
using WebApi_JWT.Repository_s;
using WebApiProduccion.Models;

namespace WebApi_JWT.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUser _user;
		protected myResponse _response; 
		public UsersController(IUser user)
		{
			_user = user;
			_response = new myResponse();

		}

		[HttpPost]
		public async Task<ActionResult>  Register(UserRegister user)
		{
			try
			{
				var resp = await _user.Register(user);
				if(resp == "user exist")
				{
					_response.DisplayMessage = "The user : " + user.UserName + "exist!"; 
					return BadRequest(_response);
				}
				
			    if (resp=="-500")
				{
					_response.DisplayMessage = "internal server error";
					return BadRequest(_response);
				}
				
				if (resp=="newUser")
				{
					_response.DisplayMessage = "Welcome ";
					UserRequest userData = new();
					userData.UserName = user.UserName;
					userData.Rol = user.Rol;
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
