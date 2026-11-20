namespace ClubDeportivo.Application.Dtos;

public class InscripcionDto
{
    public int Id { get; set; }
    public int ClubId { get; set; }
    public string? ClubTexto { get; set; }  
    public int MiembroId { get; set; }
    public string? MiembroTexto { get; set; }  
    public DateOnly FechaInscripcion { get; set; }
    public string Estado { get; set; } = "A";
}