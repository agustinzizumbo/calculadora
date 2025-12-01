namespace CalculadoraKW.Api.DTOs;

public class ActualizarAparatoDto
{
    public string Tipo { get; set; } = null!;
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public string? Tamaño { get; set; }
    public decimal ConsumoKWh { get; set; }
}
