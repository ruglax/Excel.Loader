﻿using Labs.Excel.Loader.Configuration;
using NPOI.SS.UserModel;

namespace Labs.Excel.Loader
{
    public interface IWorbookReader
    {
        CatalogFile CatalogConfiguration { get; }

        IWorkbook ReadWorkbook();
    }
}