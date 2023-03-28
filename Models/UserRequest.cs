namespace WebApi_JWT.Models
{
	public class UserRequest
	{
		   public int Id { get; set; }
        public string UserName { get; set; }

		public string JWT { get; set; }
		public string Rol { get; set; }
		public string Message { get; set; }
	}
}
