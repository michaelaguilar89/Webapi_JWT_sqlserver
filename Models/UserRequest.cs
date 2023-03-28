namespace WebApi_JWT.Models
{
	public class UserRequest
	{
        public string UserName { get; set; }

		public string JWT { get; set; }
		public string Rol { get; set; }
	}
}
