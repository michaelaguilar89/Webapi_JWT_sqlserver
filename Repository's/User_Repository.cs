using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApi_JWT.Connection;
using WebApi_JWT.Models;

namespace WebApi_JWT.Repository_s
{
	public class User_Repository : IUser
	{
		private readonly Context _db;
		private readonly IConfiguration _config;
		public User_Repository(Context db, IConfiguration config)
		{
			_db = db;
			_config = config;
		}

		public async Task<UserRequest> GetUser(string username)
		{
			try
			{
				UserRequest user = new();
				var userData = await _db.users.FirstOrDefaultAsync(
					x=>x.UserName.ToLower().Equals(username.ToLower())
					);
				user.Id = userData.Id;
				user.UserName=userData.UserName;
				user.Rol = userData.Rol;
				user.JWT = CreateToken(user.Id,user.UserName,user.Rol);
				user.Message = "...";
				return user;
			}
			catch (Exception e)
			{
				UserRequest user = new();
				user.Message = "-500 "+e.ToString();
				return user;
			}
		}
		public async Task<string> Login(UserLogin user_login)
		{
			try
			{
				//Users user = new();
			    var	user = await _db.users.FirstOrDefaultAsync(
					x=>x.UserName.ToLower() == user_login.UserName.ToLower()
					);
				if (user==null)
				{
					return "notfound";
				}
				bool passwordCorrect = VerifyPasswordHash(user_login.Password, user.PasswordHash, user.PasswordSalt);
				if (passwordCorrect==false)
				{
					return "wrongPassword";
				}
				else
				{
					return "ok";
				}
			}
			catch (Exception)
			{

				return "-500";
			}
			
		}

		public async Task<string> Register(UserRegister user)
		{
			try
			{
				string userExiste = await UserExist(user.UserName);
				switch (userExiste)
				{
					case "exist!":
						return "user exist";
						

					case "notExist":
						break;

						default:
						break;
				}


				//creating encrypting password and key
				CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] pasawordSalt);
				Users newUser = new();
				newUser.PasswordSalt = pasawordSalt;
				newUser.PasswordHash = passwordHash;
				newUser.UserName = user.UserName;
				newUser.Rol = user.Rol;
				await _db.users.AddAsync(newUser);
				await _db.SaveChangesAsync();
				return "newUser";
			}
			catch (Exception)
			{

				return "-500";
			}
		}
		// this method verify password
		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using(var hmac=new System.Security.Cryptography.HMACSHA512(passwordSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				for( int i=0;i<computedHash.Length ; i++)
				{
				 	if(computedHash[i] != passwordHash[i])
					{
						return false;
					}
					
				}
				return true;
			}
		}

		private void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] pasawordSalt)
		{

			using (var hmac = new System.Security.Cryptography.HMACSHA512())
			{
				pasawordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}

		}
		private string CreateToken(int id,string UserName,string Rol)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier,id.ToString()),
				new Claim(ClaimTypes.Name,UserName),
				new Claim(ClaimTypes.Role,Rol)
				//new Claim(ClaimTypes.)
			};
			var Keys = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
				_config.GetSection("appSettings:Token").Value));
			var Cred = new SigningCredentials(Keys, SecurityAlgorithms.HmacSha512Signature);

			var TokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = System.DateTime.UtcNow.AddHours(2),
				SigningCredentials = Cred
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(TokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
		public Task<string> Update(UserLogin user_login)
		{
			throw new NotImplementedException();
		}

		public async Task<string> UserExist(string userName)
		{
				if (await _db.users.AnyAsync(x=>x.UserName.ToLower().Equals(userName)))
				{
					return "exist!";
				}
				return "notExist";
			
			
		}
	}
}
