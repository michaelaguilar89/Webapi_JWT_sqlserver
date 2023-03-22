using System.ComponentModel.DataAnnotations;

namespace WebApi_JWT.Models
{
	public class Users
	{
		[Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public byte[] PasswordHash  { get; set; }

		public byte[] PasswordSalt { get; set; }

		public string Rol { get; set; }
    }
}
