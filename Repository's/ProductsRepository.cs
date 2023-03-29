using Microsoft.Data.SqlClient;
using WebApi_JWT.Models;

namespace WebApi_JWT.Repository_s
{
	public class ProductsRepository : IProducts
	{
		private readonly string _connectionString;
		public ProductsRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DefaultConnection");
		}

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

		public async Task<List<Products>> GetProducts()
		{
			try
			{
				using (SqlConnection sql = new SqlConnection(_connectionString))
				{
					using (SqlCommand cmd = new SqlCommand("getAllProducts",sql))
					{
						cmd.CommandType = System.Data.CommandType.StoredProcedure;
						var response = new List<Products>();
						await sql.OpenAsync();
						using (var reader = await cmd.ExecuteReaderAsync())
						{
							while (await reader.ReadAsync())
							{
								response.Add(MapToValue(reader));
							}
						}
						return response;
					}
				}
			}
			catch (Exception)
			{

				throw;
			}
		}

		private Products MapToValue(SqlDataReader reader)
		{
			return new Products()
			{
				Id = (int)reader["Id"],
				Name = (string)reader["Name"],
				Category = (string)reader["Category"],
				Price = (decimal)reader["Price"],
				Stock = (int)reader["Stock"]
			};
		}
	}
}
