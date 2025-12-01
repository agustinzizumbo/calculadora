namespace CalculadoraKW.Api.DTOs;

public class CrearUsoAparatoDto
{
    public int IdAparato { get; set; }
    public int Cantidad { get; set; }
    public decimal Horas { get; set; }
    public int Dias { get; set; }
    public string? Comentario { get; set; }
}
