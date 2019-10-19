using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Labs.Excel.Loader.Database
{
    public class Repository<T> : IRepository<T>
        where T : class, new()
    {
        private readonly DbCatalogContext _context;

        public Repository(DbCatalogContext context)
        {
            _context = context;
        }

        public void BulkInsert(T[] entities)
        {
            try
            {
                _context.BulkInsert(entities);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}
