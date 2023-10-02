using ApiFarmacia.Dtos;
using APIFarmacia.Controllers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace ApiFarmacia.Controllers;
public class PacienteController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public PacienteController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    }


    [HttpGet]
    // [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<PacienteDto>>> Get()
    {
        var Paciente = await unitofwork.Pacientes.GetAllAsync();
        return mapper.Map<List<PacienteDto>>(Paciente);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PacienteDto>> Get(int id)
    {
        var Paciente = await unitofwork.Pacientes.GetByIdAsync(id);
        if (Paciente == null)
        {
            return NotFound();
        }
        return this.mapper.Map<PacienteDto>(Paciente);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Paciente>> Post(PacienteDto pacienteDto)
    {
        var Paciente = this.mapper.Map<Paciente>(pacienteDto);
        this.unitofwork.Pacientes.Add(Paciente);
        await unitofwork.SaveAsync();
        if (Paciente == null)
        {
            return BadRequest();
        }
        pacienteDto.Id = Paciente.Id;
        return CreatedAtAction(nameof(Post), new { id = pacienteDto.Id }, pacienteDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PacienteDto>> Put(int id, [FromBody] PacienteDto pacienteDto)
    {
        if (pacienteDto == null)
        {
            return NotFound();
        }
        var Paciente = this.mapper.Map<Paciente>(pacienteDto);
        unitofwork.Pacientes.Update(Paciente);
        await unitofwork.SaveAsync();
        return pacienteDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var Paciente = await unitofwork.Pacientes.GetByIdAsync(id);
        if (Paciente == null)
        {
            return NotFound();
        }
        unitofwork.Pacientes.Remove(Paciente);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    [HttpGet("pacientequemasgasto")]
    public async Task<ActionResult<object>> ObtenerPacienteQueMasGastoAsync()
    {
        var paciente = await unitofwork.Pacientes.ObtenerPacienteQueMasGastoAsync();
        return Ok(paciente);
    }

}