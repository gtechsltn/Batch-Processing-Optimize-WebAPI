using Dapper;
using LargeDataApiAync.Data;
using LargeDataApiAync.Models;
using System.Data;
using System.Data.Common;

namespace LargeDataApiAync.Services
{
    public class ProductRepository
    {
        private readonly DapperContext _connectionString;

        public ProductRepository(DapperContext connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Record>> GetPaginatedProducts(int pageNumber, int pageSize)
        {
            using (var db = _connectionString.CreateConnection())
            {
                var sql = "SELECT * FROM Records ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";
                return await db.QueryAsync<Record>(sql, new { Offset = (pageNumber - 1) * pageSize, PageSize = pageSize });
            }
        }

        public async Task<int> GetTotalProductsCount()
        {
            using (var db = _connectionString.CreateConnection())
            {
                var sql = "SELECT COUNT(*) FROM Records;";
                return await db.ExecuteScalarAsync<int>(sql);
            }
        }

        // Asynchronous method to fetch large data with pagination
        public async Task<List<Product>> GetProductsAsync(int pageNumber, int pageSize)
        {
            using (var db = _connectionString.CreateConnection())
            {
                var sql = "SELECT * FROM Records";
                var datas = await db.QueryAsync<Product>(sql);
                
                return  datas
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            }

        }
    }
}
