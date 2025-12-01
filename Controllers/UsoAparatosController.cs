using AutoMapper;
using CalculadoraKW.Api.Data;
using CalculadoraKW.Api.DTOs;
using CalculadoraKW.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraKW.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Uso de Aparatos")]
[Produces("application/json")]
public class UsoAparatosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UsoAparatosController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>Obtiene el consumo energético detallado por uso de aparatos.</summary>
    [HttpGet("consumo")]
    [ProducesResponseType(typeof(IEnumerable<UsoAparatoDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UsoAparatoDto>>> GetConsumo()
    {
        var usos = await _context.UsoAparatos
            .Include(u => u.IdAparatoNavigation)
            .ToListAsync();

        return Ok(_mapper.Map<IEnumerable<UsoAparatoDto>>(usos));
    }

    [HttpGet("consumos")]
    public async Task<IActionResult> GetConsumos()
    {
        var consumos = await _context.UsoAparatos
            .Include(u => u.IdAparatoNavigation)
            .GroupBy(u => u.IdAparatoNavigation.Tipo)
            .Select(g => new {
                Aparato = g.Key,
                ConsumoTotal = g.Sum(x => x.Cantidad * x.Horas * x.Dias * x.IdAparatoNavigation.ConsumoKWh)
            })
            .ToListAsync();

        return Ok(consumos);
    }

    /// <summary>Obtiene un uso de aparato por su ID.</summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UsoAparatoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UsoAparatoDto>> GetById(int id)
    {
        var uso = await _context.UsoAparatos
            .Include(u => u.IdAparatoNavigation)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (uso == null) return NotFound();
        return Ok(_mapper.Map<UsoAparatoDto>(uso));
    }

    /// <summary>Registra un nuevo uso de aparato.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(UsoAparatoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UsoAparatoDto>> Create(CrearUsoAparatoDto dto)
    {
        var uso = _mapper.Map<UsoAparatos>(dto);
        _context.UsoAparatos.Add(uso);
        await _context.SaveChangesAsync();

        var usoCompleto = await _context.UsoAparatos
            .Include(u => u.IdAparatoNavigation)
            .FirstOrDefaultAsync(u => u.Id == uso.Id);

        return CreatedAtAction(nameof(GetById), new { id = uso.Id }, _mapper.Map<UsoAparatoDto>(usoCompleto));
    }

    /// <summary>Actualiza un uso de aparato existente.</summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, ActualizarUsoAparatoDto dto)
    {
        var uso = await _context.UsoAparatos.FindAsync(id);
        if (uso == null) return NotFound();

        _mapper.Map(dto, uso);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>Elimina un uso de aparato por su ID.</summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var uso = await _context.UsoAparatos.FindAsync(id);
        if (uso == null) return NotFound();

        _context.UsoAparatos.Remove(uso);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
