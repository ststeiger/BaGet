using System;
using BaGet.Core;
using BaGet.Database.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BaGet
{
    public static class MySqlApplicationExtensions
    {
        public static BaGetApplication AddMySqlDatabase(this BaGetApplication app)
        {
            app.Services.AddBaGetDbContextProvider<MySqlContext>("MySql", (provider, options) =>
            {
                var databaseOptions = provider.GetRequiredService<IOptionsSnapshot<DatabaseOptions>>().Value;

                //options.UseMySql(databaseOptions.ConnectionString);

                // options.UseMySql(databaseOptions.ConnectionString, new MySqlServerVersion("8.0"));
                options.UseMySql(databaseOptions.ConnectionString, new MariaDbServerVersion("10.6"));
            });

            return app;
        }

        public static BaGetApplication AddMySqlDatabase(
            this BaGetApplication app,
            Action<DatabaseOptions> configure)
        {
            app.AddMySqlDatabase();
            app.Services.Configure(configure);
            return app;
        }
    }
}
