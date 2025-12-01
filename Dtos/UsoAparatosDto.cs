namespace CalculadoraKW.Api.DTOs;

public class UsoAparatoDto
{
    public int Id { get; set; }
    public int IdAparato { get; set; }
    public string Tipo { get; set; } = null!;
    public string? Marca { get; set; }
    public int Cantidad { get; set; }
    public decimal Horas { get; set; }
    public int Dias { get; set; }
    public string? Comentario { get; set; }
    public decimal ConsumoKWh { get; set; }
    public decimal KWhConsumidos => Cantidad * Horas * Dias * ConsumoKWh;
}
