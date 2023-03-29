using WebApi_JWT.Models;

namespace WebApi_JWT.Repository_s
{
	public interface IProducts
	{
		Task<List<Products>> GetProducts();

		Task<Products> GetProductById(int id);

		Task<string> Delete(int id);

		Task<string> CreateUpdate(Products product);
	}
}
