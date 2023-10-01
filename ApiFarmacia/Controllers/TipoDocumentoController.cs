using ApiFarmacia.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIFarmacia.Controllers;
    public class TipoDocumentoController : ApiBaseController
    {
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public TipoDocumentoController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<TipoDocumentoDto>>> Get()
    {
        var tipoDocumento = await unitofwork.TipoDocumentos.GetAllAsync();
        return mapper.Map<List<TipoDocumentoDto>>(tipoDocumento);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TipoDocumentoDto>> Get(int id)
    {
        var tipoDocumento = await unitofwork.TipoDocumentos.GetByIdAsync(id);
        if (tipoDocumento == null)
        {
            return NotFound();
        }
        return this.mapper.Map<TipoDocumentoDto>(tipoDocumento);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<TipoDocumento>> Post(TipoDocumentoDto tipoDocumentoDto)
    {
        var tipoDocumento = this.mapper.Map<TipoDocumento>(tipoDocumentoDto);
        this.unitofwork.TipoDocumentos.Add(tipoDocumento);
        await unitofwork.SaveAsync();
        if (tipoDocumento == null)
        {
            return BadRequest();
        }
        tipoDocumentoDto.Id = tipoDocumento.Id;
        return CreatedAtAction(nameof(Post), new { id = tipoDocumentoDto.Id }, tipoDocumentoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TipoDocumentoDto>> Put(int id, [FromBody] TipoDocumentoDto tipoDocumentoDto)
    {
        if (tipoDocumentoDto == null)
        {
            return NotFound();
        }
        var tipoDocumento = this.mapper.Map<TipoDocumento>(tipoDocumentoDto);
        unitofwork.TipoDocumentos.Update(tipoDocumento);
        await unitofwork.SaveAsync();
        return tipoDocumentoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var tipoDocumento = await unitofwork.TipoDocumentos.GetByIdAsync(id);
        if (tipoDocumento == null)
        {
            return NotFound();
        }
        unitofwork.TipoDocumentos.Remove(tipoDocumento);
        await unitofwork.SaveAsync();
        return NoContent();
    }
    }