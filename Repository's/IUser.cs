using WebApi_JWT.Models;

namespace WebApi_JWT.Repository_s
{
	public interface IUser
	{
	    Task<string> Login(UserLogin user_login);

		Task<string> Register(UserRegister user);
		Task<UserRequest> GetUser(string userName);

		Task<string> Update(UserLogin user_login);

		Task<string> UserExist(string userName);
	}
}
