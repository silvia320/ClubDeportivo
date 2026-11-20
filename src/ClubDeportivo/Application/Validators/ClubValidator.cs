using FluentValidation;
using ClubDeportivo.Application.Abstractions;
using ClubDeportivo.Application.Dtos;
using ClubDeportivo.Domain.Entities;

namespace ClubDeportivo.Application.Validators;

public class ClubValidator : AbstractValidator<ClubDto>
{
    public ClubValidator(IRepository<Club> self)
    {
        RuleFor(x => x.Nombre).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Nombre: campo obligatorio")
            .MaximumLength(80).WithMessage("Nombre: máximo 80 caracteres")
            .MustAsync(async (dto, v, ct) => !await self.AnyAsync(e => e.Nombre.ToLower() == v.Trim().ToLower() && e.Id != dto.Id, ct))
            .WithMessage("Nombre: ya existe un registro con ese valor");

        RuleFor(x => x.Deporte).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Deporte: campo obligatorio")
            .MaximumLength(60).WithMessage("Deporte: máximo 60 caracteres");

        RuleFor(x => x.Ubicacion).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Ubicación: campo obligatorio")
            .MaximumLength(120).WithMessage("Ubicación: máximo 120 caracteres");

        RuleFor(x => x.FechaFundacion).Cascade(CascadeMode.Stop)
            .Must(f => f >= new DateOnly(1900, 1, 1)).WithMessage("Fecha de fundación no es válida")
            .Must(f => f <= DateOnly.FromDateTime(DateTime.Today)).WithMessage("Fecha de fundación no puede ser futura");
    }
}