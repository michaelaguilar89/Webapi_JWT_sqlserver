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

		public async Task<string> CreateUpdate(Products product)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				if (product.Id > 0)
				{
					using (SqlCommand cmd = new SqlCommand("sp_updateProduct", sql))
					{
						cmd.CommandType = System.Data.CommandType.StoredProcedure;
						cmd.Parameters.Add(new SqlParameter("@Id", product.Id));
						cmd.Parameters.Add(new SqlParameter("@Name", product.Name));
						cmd.Parameters.Add(new SqlParameter("@Category", product.Category));
						cmd.Parameters.Add(new SqlParameter("@Price", product.Price));
						cmd.Parameters.Add(new SqlParameter("@Stock", product.Stock));
						await sql.OpenAsync();
						await cmd.ExecuteNonQueryAsync();
						return "update!";
					}
				}
				else
				{
					using (SqlCommand cmd = new SqlCommand("sp_insertProducts", sql))
					{
						cmd.CommandType = System.Data.CommandType.StoredProcedure;
						cmd.Parameters.Add(new SqlParameter("@Name",product.Name));
						cmd.Parameters.Add(new SqlParameter("@Category", product.Category));
						cmd.Parameters.Add(new SqlParameter("@Price", product.Price));
						cmd.Parameters.Add(new SqlParameter("@Stock", product.Stock));
						await sql.OpenAsync();
						await cmd.ExecuteNonQueryAsync();
						return "insert!";
					}
				}
			}
		}

		public async Task<string> Delete(int id)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand("sp_deleteProduct", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@Id", id));
					await sql.OpenAsync();
					await cmd.ExecuteNonQueryAsync();
					return "delete!";
				}
			}
		}

		public async Task<Products> GetProductById(int id)
		{
			using (SqlConnection sql = new SqlConnection(_connectionString))
			{
				using(SqlCommand cmd = new SqlCommand("sp_getProductById", sql))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.Parameters.Add(new SqlParameter("@Id", id));
					Products product = null;
					await sql.OpenAsync();
					using (var reader= await cmd.ExecuteReaderAsync())
					{
						while (await reader.ReadAsync())
						{
							product = MapToValue(reader);
						}
					}
					return product;
				}
			}
		}

		public async Task<List<Products>> GetProducts()
		{
			
			
				using (SqlConnection sql = new SqlConnection(_connectionString))
				{
					using (SqlCommand cmd = new SqlCommand("sp_getAllProducts",sql))
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
