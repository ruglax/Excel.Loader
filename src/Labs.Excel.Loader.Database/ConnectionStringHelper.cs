namespace Labs.Excel.Loader.Database
{
    public class ConnectionStringHelper
    {
        private readonly string _connectionString;

        public ConnectionStringHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
