using ApiFarmacia.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIFarmacia.Controllers;
    public class TipoContactoController : ApiBaseController
    {
        private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public TipoContactoController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<TipoContactoDto>>> Get()
    {
        var tipoContacto = await unitofwork.TipoContactos.GetAllAsync();
        return mapper.Map<List<TipoContactoDto>>(tipoContacto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TipoContactoDto>> Get(int id)
    {
        var tipoContacto = await unitofwork.TipoContactos.GetByIdAsync(id);
        if (tipoContacto == null)
        {
            return NotFound();
        }
        return this.mapper.Map<TipoContactoDto>(tipoContacto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<TipoContacto>> Post(TipoContactoDto tipoContactoDto)
    {
        var tipoContacto = this.mapper.Map<TipoContacto>(tipoContactoDto);
        this.unitofwork.TipoContactos.Add(tipoContacto);
        await unitofwork.SaveAsync();
        if (tipoContacto == null)
        {
            return BadRequest();
        }
        tipoContactoDto.Id = tipoContacto.Id;
        return CreatedAtAction(nameof(Post), new { id = tipoContactoDto.Id }, tipoContactoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TipoContactoDto>> Put(int id, [FromBody] TipoContactoDto tipoContactoDto)
    {
        if (tipoContactoDto == null)
        {
            return NotFound();
        }
        var tipoContacto = this.mapper.Map<TipoContacto>(tipoContactoDto);
        unitofwork.TipoContactos.Update(tipoContacto);
        await unitofwork.SaveAsync();
        return tipoContactoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var tipoContacto = await unitofwork.TipoContactos.GetByIdAsync(id);
        if (tipoContacto == null)
        {
            return NotFound();
        }
        unitofwork.TipoContactos.Remove(tipoContacto);
        await unitofwork.SaveAsync();
        return NoContent();
    }
    }