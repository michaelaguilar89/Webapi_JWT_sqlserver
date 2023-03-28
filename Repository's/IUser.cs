using WebApi_JWT.Models;

namespace WebApi_JWT.Repository_s
{
	public interface IUser
	{
		Task<string> Login(UserLogin user_login);

		Task<string> Register(Users user);

		Task<string> Update(UserLogin user_login);
	}
}
