using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Labs.Excel.Loader.Database
{
    public class IndexHelper : IIndexHelper
    {
        private readonly ILogger<IndexHelper> _logger;

        private readonly string _connectionString;

        private const string IndexQuery =
            "SELECT ClusteredIndexName = i.name, ColumnName = c.Name " +
            "FROM sys.tables t " +
            "INNER JOIN  sys.indexes i ON t.object_id = i.object_id " +
            "INNER JOIN  sys.index_columns ic ON i.index_id = ic.index_id AND i.object_id = ic.object_id " +
            "INNER JOIN  sys.columns c ON ic.column_id = c.column_id AND ic.object_id = c.object_id " +
            "WHERE i.index_id = 1 AND t.name = @table";

        private const string DropIndexQuery = "DROP INDEX {0} ON {1}";// indexName + tableName

        //private const string CreateIndexQuey = "CREATE CLUSTERED INDEX {0} ON {1} ({2})"; // indexName + tableName + columnName
        private const string CreateIndexQuey = "ALTER TABLE {0} ADD CONSTRAINT {1} PRIMARY KEY CLUSTERED({2})";//  tableName  + indexName+ columnName

        private const string DropConstraintQuery = "ALTER TABLE {0} DROP CONSTRAINT {1}"; // table + indexName
        public IndexHelper(ILogger<IndexHelper> logger, ConnectionStringHelper connectionStringHelperHelper)
        {
            _logger = logger;
            _connectionString = connectionStringHelperHelper.GetConnectionString();
        }


        public dynamic GetClusteredIndex(string table)
        {
            using (SqlConnection connection =
                new SqlConnection(_connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(IndexQuery, connection);
                command.Parameters.AddWithValue("@table", table);

                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        return new
                        {
                            ClusteredIndexName = reader[0],
                            ColumnName = reader[1]
                        };
                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "No fue posible encontrar el indice clusterizado", table);
                }

                return null;
            }
        }

        public void DeleteIndex(string table, string index, string column)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            { SqlCommand command = new SqlCommand(string.Format(DropIndexQuery, index, table), connection);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "No fue posible eliminar el índice clusterizado", table);
                }
            }
        }

        public void CreateIndex(string table, string index, string column)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(string.Format(CreateIndexQuey, table, index, column), connection);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "No fue posible crear el índice clusterizado", table);
                }
            }
        }

        public void DropConstraint(string table, string index)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(string.Format(DropConstraintQuery, table, index), connection);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "No fue posible eliminar el constraint del PK", table);
                }
            }
        }
    }
}
