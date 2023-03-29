using WebApi_JWT.Models;

namespace WebApi_JWT.Repository_s
{
	public class ProductsRepository : IProducts
	{
		public Task<string> CreateUpdate(Products product)
		{
			throw new NotImplementedException();
		}

		public Task<string> Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<Products> GetProductById(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<Products>> GetProducts()
		{
			throw new NotImplementedException();
		}
	}
}
