using AutoMapper;
using CalculadoraKW.Api.Data;
using CalculadoraKW.Api.DTOs;
using CalculadoraKW.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraKW.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Aparatos")]
[Produces("application/json")]
public class AparatosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AparatosController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>Obtiene todos los aparatos registrados.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AparatoDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AparatoDto>>> GetAll()
    {
        var aparatos = await _context.Aparatos.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<AparatoDto>>(aparatos));
    }

    /// <summary>Obtiene un aparato por su ID.</summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AparatoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AparatoDto>> GetById(int id)
    {
        var aparato = await _context.Aparatos.FindAsync(id);
        if (aparato == null) return NotFound();
        return Ok(_mapper.Map<AparatoDto>(aparato));
    }

    /// <summary>Crea un nuevo aparato.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(AparatoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AparatoDto>> Create(CrearAparatoDto dto)
    {
        var aparato = _mapper.Map<Aparatos>(dto);
        _context.Aparatos.Add(aparato);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = aparato.Id }, _mapper.Map<AparatoDto>(aparato));
    }

    /// <summary>Actualiza un aparato existente.</summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, ActualizarAparatoDto dto)
    {
        var aparato = await _context.Aparatos.FindAsync(id);
        if (aparato == null) return NotFound();

        _mapper.Map(dto, aparato);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>Elimina un aparato por su ID.</summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var aparato = await _context.Aparatos.FindAsync(id);
        if (aparato == null) return NotFound();

        _context.Aparatos.Remove(aparato);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
