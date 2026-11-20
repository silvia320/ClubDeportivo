namespace ClubDeportivo.Application.Dtos;
public class ClubDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Deporte { get; set; } = string.Empty;
    public string Ubicacion { get; set; } = string.Empty;
    public DateOnly FechaFundacion { get; set; }
    public int MiembrosActivos { get; set; }
      
    public string Texto => Nombre;
}