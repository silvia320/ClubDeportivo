namespace ClubDeportivo.Application.Dtos;

public class MiembroDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public int ClubesActivos { get; set; }
   
    public string Texto => string.Join(" ", Nombre, Apellido);
}