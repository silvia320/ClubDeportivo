using Microsoft.EntityFrameworkCore;
using Npgsql;
using ClubDeportivo.Application.Abstractions;
using ClubDeportivo.Infrastructure.Persistence;

namespace ClubDeportivo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = BuildConnectionString(config);

        services.AddDbContext<AppDbContext>(opt =>
            opt.UseNpgsql(connectionString, npgsql =>
                npgsql.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }

    // Misma logica en Docker y en Visual Studio: todo viene de variables de entorno (.env en VS, compose en Docker).
    private static string BuildConnectionString(IConfiguration config)
    {
        var full = config.GetConnectionString("Default");
        if (!string.IsNullOrWhiteSpace(full)) return full;

        return new NpgsqlConnectionStringBuilder
        {
            Host = config["DB_HOST"] ?? "localhost",
            Port = int.TryParse(config["DB_PORT"], out var p) ? p : 5432,
            Database = config["DB_NAME"] ?? throw new InvalidOperationException("Falta DB_NAME (ver sample.env)"),
            Username = config["DB_USER"] ?? throw new InvalidOperationException("Falta DB_USER (ver sample.env)"),
            Password = config["DB_PASSWORD"] ?? throw new InvalidOperationException("Falta DB_PASSWORD (ver sample.env)")
        }.ConnectionString;
    }

    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        if (!string.Equals(app.Configuration["AUTO_MIGRATE"] ?? "true", "true", StringComparison.OrdinalIgnoreCase))
            return;

        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Reintentos: en Docker la BD puede tardar unos segundos en aceptar conexiones.
        for (var attempt = 1; ; attempt++)
        {
            try { await db.Database.MigrateAsync(); await DbSeeder.SeedAsync(db); return; }
            catch (NpgsqlException) when (attempt < 10) { await Task.Delay(TimeSpan.FromSeconds(2)); }
        }
    }
}