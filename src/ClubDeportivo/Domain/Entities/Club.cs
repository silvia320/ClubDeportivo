using ClubDeportivo.Domain.Common;

namespace ClubDeportivo.Domain.Entities;


public class Club : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string Deporte { get; set; } = string.Empty;
    public string Ubicacion { get; set; } = string.Empty;
    public DateOnly FechaFundacion { get; set; }
    public ICollection<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
    
    public int MiembrosActivos => Inscripciones.Count(i => i.Estado == "A");
}