using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Labs.Excel.Loader.Database
{
    public class Repository<T> : IRepository<T>
        where T : class, new()
    {
        private readonly ILogger<Repository<T>> _logger;

        private static readonly ConcurrentDictionary<string, PropertyInfo[]> PropertyInfoCache =
            new ConcurrentDictionary<string, PropertyInfo[]>();

        private readonly ConnectionStringHelper _connectionStringHelper;

        private readonly string _key;

        public Repository(ConnectionStringHelper connectionStringHelperHelper, ILogger<Repository<T>> logger)
        {
            _connectionStringHelper = connectionStringHelperHelper;
            _logger = logger;
            _key = typeof(T).Name;
            Init();
        }

        public void BulkInsert(T[] entities)
        {
            try
            {
                _logger.LogDebug($"Saving records {entities.Length} of type {_key}");
                if (PropertyInfoCache.TryGetValue(_key, out var properties))
                {
                    var table = CreateDataTable(properties);

                    FillDataTable(table, properties, entities);
                    using (SqlBulkCopy copy = new SqlBulkCopy(_connectionStringHelper.GetConnectionString()))
                    {
                        copy.BulkCopyTimeout = 120;
                        copy.DestinationTableName = _key;
                        copy.WriteToServer(table);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error saving records {entities.Length} of type {_key}");
            }
        }

        private void Init()
        {
            if (!PropertyInfoCache.TryGetValue(_key, out var properties))
            {
                properties = typeof(T).GetProperties();
                PropertyInfoCache.TryAdd(_key, properties);
            }
        }

        private DataTable CreateDataTable(PropertyInfo[] properties)
        {
            var table = new DataTable();
            foreach (var property in properties)
            {
                bool allowDbNull = false;
                var propertyType = property.PropertyType;
                if (propertyType.IsGenericType &&
                    propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propertyType = propertyType.GetGenericArguments()[0];
                    allowDbNull = true;
                }

                DataColumn column = new DataColumn(property.Name, propertyType) { AllowDBNull = allowDbNull };
                table.Columns.Add(column);
            }

            return table;
        }

        private void FillDataTable(DataTable table, PropertyInfo[] properties, T[] entities)
        {
            foreach (var entity in entities)
            {
                table.Rows.Add(properties.Select(p => p.GetValue(entity, null)).ToArray());
            }
        }
    }
}
