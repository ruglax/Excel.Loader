using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Labs.Excel.Loader.Model;
using Microsoft.Extensions.Logging;

namespace Labs.Excel.Loader.Database
{
    public class Repository<T> : IRepository<T>, IRepositoryAsync<T>
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

        public void BulkInsertAsync(T[] entities)
        {
            Bulk(entities, async (table, percent) =>
            {
                using (SqlBulkCopy copy = new SqlBulkCopy(_connectionStringHelper.GetConnectionString()))
                {
                    copy.BulkCopyTimeout = 300;
                    copy.DestinationTableName = _key;
                    copy.NotifyAfter = percent;
                    copy.SqlRowsCopied += (sender, args) => _logger.LogInformation($"Processing  {args.RowsCopied} records of type {_key}");
                    await copy.WriteToServerAsync(table);
                }
            });
        }

        public void BulkInsert(T[] entities)
        {
            Bulk(entities, (table, percent) =>
            {
                using (SqlBulkCopy copy = new SqlBulkCopy(_connectionStringHelper.GetConnectionString()))
                {
                    copy.BulkCopyTimeout = 300;
                    copy.DestinationTableName = _key;
                    copy.NotifyAfter = percent;
                    copy.SqlRowsCopied += (sender, args) => _logger.LogInformation($"Processing  {args.RowsCopied} records of type {_key}");
                    copy.WriteToServer(table);
                }
            });
        }

        public void Bulk(T[] entities, Action<DataTable, int> action)
        {
            try
            {
                if (!entities.Any())
                {
                    _logger.LogInformation($"No entities of type {_key} to save");
                    return;
                }

                var percent = entities.Length / 10;
                _logger.LogInformation($"Saving records {entities.Length} of type {_key}");
                if (PropertyInfoCache.TryGetValue(_key, out var properties))
                {
                    DataTable table = CreateDataTable(properties);
                    FillDataTable(table, properties, entities);
                    action.Invoke(table, percent);
                    _logger.LogInformation($"Saved  {entities.Length} records of type {_key}");
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
