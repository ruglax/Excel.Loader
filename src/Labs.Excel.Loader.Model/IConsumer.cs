using System;
using System.Collections.Generic;
using System.Text;

namespace Labs.Excel.Loader.Model
{
    public interface IConsumer
    {
        void Transform(Message message);
    }
}
