using System;
using System.Collections.Generic;

namespace CalculadoraKW.Api.Models;

public partial class UsoAparatos
{
    public int Id { get; set; }

    public int IdAparato { get; set; }

    public int Cantidad { get; set; }

    public decimal Horas { get; set; }

    public int Dias { get; set; }

    public string? Comentario { get; set; }

    public virtual Aparatos IdAparatoNavigation { get; set; } = null!;
}
