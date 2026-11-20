using FluentValidation;
using ClubDeportivo.Application.Abstractions;
using ClubDeportivo.Application.Services;

namespace ClubDeportivo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Registra automaticamente todos los AbstractValidator<T> del proyecto.
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddScoped<IClubService, ClubService>();
        services.AddScoped<IMiembroService, MiembroService>();
        services.AddScoped<IInscripcionService, InscripcionService>();
        return services;
    }
}