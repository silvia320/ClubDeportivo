using FluentValidation;
using ClubDeportivo.Application.Abstractions;
using ClubDeportivo.Application.Dtos;
using ClubDeportivo.Domain.Entities;

namespace ClubDeportivo.Application.Validators;

public class InscripcionValidator : AbstractValidator<InscripcionDto>
{
    public InscripcionValidator(IRepository<Inscripcion> self, IRepository<Club> repoClub, IRepository<Miembro> repoMiembro)
    {
        RuleFor(x => x.ClubId).Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("Seleccione Club")
            .MustAsync(async (v, ct) => await repoClub.GetByIdAsync(v, ct) is not null)
            .WithMessage("Club: el valor seleccionado no existe");

        RuleFor(x => x.MiembroId).Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("Seleccione Miembro")
            .MustAsync(async (v, ct) => await repoMiembro.GetByIdAsync(v, ct) is not null)
            .WithMessage("Miembro: el valor seleccionado no existe");

        RuleFor(x => x.FechaInscripcion).Cascade(CascadeMode.Stop)
            .Must(f => f >= new DateOnly(1900, 1, 1)).WithMessage("Fecha de inscripción no es válida")
            .Must(f => f <= DateOnly.FromDateTime(DateTime.Today)).WithMessage("Fecha de inscripción no puede ser futura");

        RuleFor(x => x.Estado)
            .Must(v => new[] { "A", "S", "I" }.Contains(v)).WithMessage("Seleccione Estado");

        // Regla compuesta: no se repite la pareja (ClubId, MiembroId).
        RuleFor(x => x.MiembroId)
            .MustAsync(async (dto, v, ct) => !await self.AnyAsync(e => e.ClubId == dto.ClubId && e.MiembroId == v && e.Id != dto.Id, ct))
            .When(x => x.ClubId > 0 && x.MiembroId > 0)
            .WithMessage("Inscripción: este miembro ya está inscrito en ese club");
    }
}