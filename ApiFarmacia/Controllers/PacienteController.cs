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

    public async Task<ActionResult<PacienteDto>> Get11(int id)
    {
        var Paciente = await unitofwork.Pacientes.GetByIdAsync(id);
        if (Paciente == null)
        {
            return NotFound();
        }
        return this.mapper.Map<PacienteDto>(Paciente);
    }
    [HttpGet("GetInfoPacientesCompraMedicamento")]
    public async Task<IActionResult> GetInfoPacientesCompraMedicamento()
    {
        var Paciente = await unitofwork.Pacientes.GetInfoPacientesCompraMedicamento();
        if (Paciente == null)
        {
            return NotFound(); // Devuelve 404 si no se encuentra el recurso.
        }
        return Ok(this.mapper.Map<IEnumerable<Object>>(Paciente)); // Devuelve la colección si se encontró.
    }
    [HttpGet("consulta33")]
    public async Task<object> TotalGastadoPorPacienteEn2023Async()
    {
        var Paciente = await unitofwork.Pacientes.TotalGastadoPorPacienteEn2023Async();
        if (Paciente == null)
        {
            return NotFound(); // Devuelve 404 si no se encuentra el recurso.
        }
        return Ok(this.mapper.Map<Object>(Paciente)); // Devuelve la colección si se encontró.
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> ObtenerPacienteConMayorGastoAsync()
    {
        var pacienteConMayorGasto = await unitofwork.Pacientes.ObtenerPacienteConMayorGastoAsync();
        return Ok(pacienteConMayorGasto);
    }

    [HttpGet("pacientesconparacetamol")]

    public async Task<ActionResult<object>> ObtenerPacientesConParacetamolAsync()
    {
        var pacientesConParacetamol = await unitofwork.Pacientes.ObtenerPacientesConParacetamolAsync();
        return Ok(pacientesConParacetamol);
    }

    [HttpGet("pacientesSinCompras")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Paciente>>> ObtenerPacientesSinComprasEn2023Async()
    {
        var pacientes = await unitofwork.Pacientes.ObtenerPacientesSinComprasEn2023Async();
        return Ok(pacientes);
    }




}