using ClubDeportivo.Domain.Common;

namespace ClubDeportivo.Domain.Entities;


public class Miembro : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    
    public int ClubesActivos => Inscripciones.Count(i => i.Estado == "A");
}