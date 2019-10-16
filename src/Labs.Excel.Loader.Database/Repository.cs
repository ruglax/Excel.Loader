using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Labs.Excel.Loader.Database
{
    public class Repository<T> where T : class, new()
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public void BulkInsert(T[] entities)
        {
            _context.BulkInsert(entities);
        }
    }
}
