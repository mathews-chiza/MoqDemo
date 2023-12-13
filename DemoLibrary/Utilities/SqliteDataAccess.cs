using System.Data;
using Microsoft.Data.Sqlite;
using Dapper;

namespace DemoLibrary.Utilities
{
    public class SqliteDataAccess : ISqliteDataAccess
    {
        public List<T> LoadData<T>(string sql)
        {
            using (IDbConnection conn = new SqliteConnection(LoadConnectionString()))
            {
                var output = conn.Query<T>(sql, new DynamicParameters());
                return output.ToList();
            }
        }

        public void SaveData<T>(T person, string sql)
        {
            using (IDbConnection conn = new SqliteConnection(LoadConnectionString()))
            {
                conn.Execute(sql, person);
            }
        }

        private string LoadConnectionString(string key = "Default")
        {
            // TODO: Use app config for production app
            return "Data Source=.\\MoqDemoDb.db;Cache=Shared";
        }
    }
}
