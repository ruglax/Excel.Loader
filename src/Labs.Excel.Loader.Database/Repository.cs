using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Labs.Excel.Loader.Database
{
    public class Repository<T> : IRepository<T>
        where T : class, new()
    {
        private readonly ILogger<Repository<T>> _logger;

        private readonly DbCatalogContext _context;

        public Repository(DbCatalogContext context, ILogger<Repository<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task BulkInsert(T[] entities)
        {
            try
            {
                _logger.LogDebug($"Saving records {entities.Length} of type {typeof(T).Name}");
                await _context.BulkInsertAsync(entities);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error saving records {entities.Length} of type {typeof(T).Name}");
            }
        }
    }
}
