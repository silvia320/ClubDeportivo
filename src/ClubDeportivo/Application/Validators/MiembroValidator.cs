using FluentValidation;
using ClubDeportivo.Application.Abstractions;
using ClubDeportivo.Application.Dtos;
using ClubDeportivo.Domain.Entities;

namespace ClubDeportivo.Application.Validators;

public class MiembroValidator : AbstractValidator<MiembroDto>
{
    public MiembroValidator(IRepository<Miembro> self)
    {
        RuleFor(x => x.Nombre).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Nombre: campo obligatorio")
            .MaximumLength(60).WithMessage("Nombre: máximo 60 caracteres");

        RuleFor(x => x.Apellido).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Apellido: campo obligatorio")
            .MaximumLength(60).WithMessage("Apellido: máximo 60 caracteres");

        RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Correo: campo obligatorio")
            .MaximumLength(120).WithMessage("Correo: máximo 120 caracteres")
            .EmailAddress().WithMessage("El correo no tiene un formato válido")
            .MustAsync(async (dto, v, ct) => !await self.AnyAsync(e => e.Email.ToLower() == v.Trim().ToLower() && e.Id != dto.Id, ct))
            .WithMessage("Correo: ya existe un registro con ese valor");

        RuleFor(x => x.Telefono).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Teléfono: campo obligatorio")
            .MaximumLength(15).WithMessage("Teléfono: máximo 15 caracteres")
            .Matches(@"^[0-9+\-\s]{7,15}$").WithMessage("El teléfono debe tener entre 7 y 15 caracteres (dígitos, +, -)");
    }
}