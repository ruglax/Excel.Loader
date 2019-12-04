using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Labs.Excel.Loader.Model
{
    public interface IRepositoryAsync<T> where T : class, new()
    {
        void BulkInsertAsync(T[] entities);
    }
}
