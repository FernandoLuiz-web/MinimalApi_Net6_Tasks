using System.Data.SqlClient;
using static ApiTarefa.Data.TaskContext;

namespace ApiTarefa.Extensions;

public static class ServiceCollectionsExtensions
{

    public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("defaultConnection");

        builder.Services.AddScoped<GetConnection>(sp =>
        async () =>
        {
            var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        });

        return builder;
    }

}
