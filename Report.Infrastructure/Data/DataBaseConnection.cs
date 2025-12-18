using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;


namespace Report.Infrastructure.Data
{
    public sealed class DataBaseConnection
    {
        private static DataBaseConnection? _instance;
        private static readonly object _lock = new object();
        private readonly string _connectionString;

        private DataBaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static void Initialize(IConfiguration configuration)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    var connStr = configuration.GetConnectionString("DefaultConnection")!;
                    _instance = new DataBaseConnection(connStr);
                }
            }
        }

        public static DataBaseConnection Instance
        {
            get
            {
                if (_instance == null)
                    throw new InvalidOperationException("DatabaseConnection no fue inicializado. Llama a Initialize() en Program.cs");

                return _instance;
            }
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
