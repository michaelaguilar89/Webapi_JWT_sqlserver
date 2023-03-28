using Microsoft.EntityFrameworkCore;
using WebApi_JWT.Connection;
using WebApi_JWT.Models;

namespace WebApi_JWT.Repository_s
{
	public class User_Repository : IUser
	{
		private readonly Context _db;
		public User_Repository(Context db)
		{
			_db = db;
		}

		public async Task<string> Login(UserLogin user_login)
		{
			try
			{
			 var user= await _db.users.AnyAsync(x=>x.UserName == user_login.UserName);
				if (user==null)
				{
					return "userName not Found";
				}
				return "";
			}
			catch (Exception)
			{

				return "-500";
			}
			return "";
		}

		public async Task<string> Register(UserLogin user,string rol)
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
				newUser.Rol= rol;
				await _db.users.AddAsync(newUser);
				await _db.SaveChangesAsync();
				return "newUser";
			}
			catch (Exception)
			{

				return "-500";
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
