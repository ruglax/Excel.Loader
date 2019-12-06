using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Labs.Excel.Loader.Model;
using Microsoft.Extensions.Logging;

namespace Labs.Excel.Loader.Database
{
    public class Repository<T> : IRepository<T>, IRepositoryAsync<T>
        where T : class, new()
    {
        public IIndexHelper IndexHelper { get; }
        private readonly ILogger<Repository<T>> _logger;

        private static readonly ConcurrentDictionary<string, PropertyInfo[]> PropertyInfoCache =
            new ConcurrentDictionary<string, PropertyInfo[]>();

        private readonly ConnectionStringHelper _connectionStringHelper;

        private readonly IIndexHelper _indexHelper;

        private readonly string _table;

        public Repository(ILogger<Repository<T>> logger, ConnectionStringHelper connectionStringHelperHelper, IIndexHelper indexHelper)
        {
            _indexHelper = indexHelper;
            _connectionStringHelper = connectionStringHelperHelper;
            _logger = logger;
            _table = typeof(T).Name;
            Init();
        }

        /// <summary>
        /// Realiza una inserción con bulk copy asíncrono de las entidades proporcionadas.
        /// </summary>
        /// <param name="entities"></param>
        public void BulkInsertAsync(T[] entities)
        {
            Bulk(entities, async (table, percent) =>
            {
                using (var copy = CreateBulkObject(percent, table))
                {
                    await copy.WriteToServerAsync(table);
                }
            });
        }

        /// <summary>
        /// Realiza una inserción con bulk copy de las entidades proporcionadas.
        /// </summary>
        /// <param name="entities"></param>
        public void BulkInsert(T[] entities)
        {
            Bulk(entities, (table, percent) =>
            {
                using (var copy = CreateBulkObject(percent, table))
                {
                    copy.WriteToServer(table);
                }
            });
        }

        private void Bulk(T[] entities, Action<DataTable, int> action)
        {
            try
            {
                if (!entities.Any())
                {
                    _logger.LogInformation($"No entities of type {_table} to save");
                    return;
                }

                var percent = entities.Length / 10;
                var index = _indexHelper.GetClusteredIndex(_table);
                if (index != null)
                {
                    _indexHelper?.DropConstraint(_table, index.ClusteredIndexName);
                }

                _logger.LogInformation($"Saving records {entities.Length} of type {_table}");
                if (PropertyInfoCache.TryGetValue(_table, out var properties))
                {
                    DataTable table = CreateDataTable(properties);
                    FillDataTable(table, properties, entities);
                    action.Invoke(table, percent);
                    if (index != null)
                    {
                        _indexHelper.CreateIndex(_table, index.ClusteredIndexName, index.ColumnName);
                    }

                    _logger.LogInformation($"Saved  {entities.Length} records of type {_table}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error saving records {entities.Length} of type {_table}");
            }
        }

        private void Init()
        {
            if (!PropertyInfoCache.TryGetValue(_table, out _))
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                PropertyInfoCache.TryAdd(_table, properties);
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

        private SqlBulkCopy CreateBulkObject(int percent, DataTable table)
        {
            SqlBulkCopy copy = new SqlBulkCopy(_connectionStringHelper.GetConnectionString())
            {
                BulkCopyTimeout = 300,
                DestinationTableName = _table,
                NotifyAfter = percent
            };

            copy.SqlRowsCopied += (sender, args) => _logger.LogInformation($"Processing  {args.RowsCopied} records of type {_table}");
            return copy;
        }
    }
}
