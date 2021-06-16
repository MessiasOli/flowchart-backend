using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Agricultural_Plan
{
    public class DatabaseConnection
    {
        public static IConfiguration connectionConfiguration
        {
            get
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("dbsettings.json")
                    .Build();
                return configuration;
            }
        }

        public static Action<DbContextOptionsBuilder> contextOptions
        {
            get
            {
                DbContextOptionsBuilder contextOptionsBuilder = new DbContextOptionsBuilder();
                string stDBEngine = connectionConfiguration.GetSection("DatabaseSettings").GetSection("DBEngine").Value;
                return new Action<DbContextOptionsBuilder>(options => options.UseNpgsql(DatabaseConnection.connectionConfiguration
                                .GetConnectionString("PostgreSQLConnection")));
            }
        }
    }
}
