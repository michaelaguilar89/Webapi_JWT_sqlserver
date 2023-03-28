namespace WebApiProduccion.Models
{
	public class myResponse
	{
		public bool IsSucces { get; set; } = true;

		public Object? Result { get; set; } 

		public string? DisplayMessage { get; set; }

		public List<string>? ErrorMessages { get; set; }
	}
}
