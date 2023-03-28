using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_JWT.Models
{
	public class Products
	{
		[Key]
        public int Id { get; set; }
	//	[ForeignKey(nameof(Users.Id))]
		//public int IdUser { get; set; }
		public string  Name { get; set; }

		public string Category { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
