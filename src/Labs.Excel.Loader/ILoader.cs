﻿using System;
using System.Threading.Tasks.Dataflow;
using Labs.Excel.Loader.Model;

namespace Labs.Excel.Loader
{
    public interface ILoader
    {
        BufferBlock<Message> BufferBlock { get; }

        void ConfigureEntity<T>(Func<Message, T> action, Action<T[]> execution, int batchSize, DataflowLinkOptions linkOptions)
            where T : class;


        void UploadFile();
    }
}