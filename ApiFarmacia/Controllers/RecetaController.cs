
using ApiFarmacia.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIFarmacia.Controllers;
    public class RecetaController : ApiBaseController
    {
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public RecetaController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<RecetaDto>>> Get()
    {
        var receta = await unitofwork.Recetas.GetAllAsync();
        return mapper.Map<List<RecetaDto>>(receta);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<RecetaDto>> Get(int id)
    {
        var receta = await unitofwork.Recetas.GetByIdAsync(id);
        if (receta == null)
        {
            return NotFound();
        }
        return this.mapper.Map<RecetaDto>(receta);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Receta>> Post(RecetaDto recetaDto)
    {
        var receta = this.mapper.Map<Receta>(recetaDto);
        this.unitofwork.Recetas.Add(receta);
        await unitofwork.SaveAsync();
        if (receta == null)
        {
            return BadRequest();
        }
        recetaDto.Id = receta.Id;
        return CreatedAtAction(nameof(Post), new { id = recetaDto.Id }, recetaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<RecetaDto>> Put(int id, [FromBody] RecetaDto recetaDto)
    {
        if (recetaDto == null)
        {
            return NotFound();
        }
        var receta = this.mapper.Map<Receta>(recetaDto);
        unitofwork.Recetas.Update(receta);
        await unitofwork.SaveAsync();
        return recetaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var receta = await unitofwork.Recetas.GetByIdAsync(id);
        if (receta == null)
        {
            return NotFound();
        }
        unitofwork.Recetas.Remove(receta);
        await unitofwork.SaveAsync();
        return NoContent();
    }



    [HttpGet("consulta4")]
    public async Task<IActionResult>  ObtenerRecetasEmitidasDespuesDeFechaAsync()
    {
        var receta = await unitofwork.Recetas.ObtenerRecetasEmitidasDespuesDeFechaAsync();

        if (receta == null || !receta.Any())
        {
            return NotFound(); // Devuelve 404 si la colección está vacía.
        }

        return Ok(receta); // Devuelve la colección si no está vacía.
    }

    
}