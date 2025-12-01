using System;
using System.Collections.Generic;

namespace CalculadoraKW.Api.Models;

public partial class Aparatos
{
    public int Id { get; set; }

    public string Tipo { get; set; } = null!;

    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public string? Tamaño { get; set; }

    public decimal ConsumoKWh { get; set; }

    public virtual ICollection<UsoAparatos> UsoAparatos { get; set; } = new List<UsoAparatos>();
}
