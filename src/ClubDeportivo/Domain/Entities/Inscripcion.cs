using ClubDeportivo.Domain.Common;

namespace ClubDeportivo.Domain.Entities;

public class Inscripcion : BaseEntity
{
    public int ClubId { get; set; }
    public Club? Club { get; set; }
    public int MiembroId { get; set; }
    public Miembro? Miembro { get; set; }
    public DateOnly FechaInscripcion { get; set; }
    public string Estado { get; set; } = "A";
}