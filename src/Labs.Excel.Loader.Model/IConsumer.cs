using System;
using System.Collections.Generic;
using System.Text;

namespace Labs.Excel.Loader.Model
{
    public interface IConsumer
    {
        T Transform<T>(Message message) where T : class;
    }
}
