using ApiFarmacia.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIFarmacia.Controllers;

public class MedicamentoController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public MedicamentoController(IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<MedicamentoDto>>> Get()
    {
        var medicamento = await unitofwork.Medicamentos.GetAllAsync();
        return mapper.Map<List<MedicamentoDto>>(medicamento);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<MedicamentoDto>> Get(int id)
    {
        var medicamento = await unitofwork.Medicamentos.GetByIdAsync(id);
        if (medicamento == null)
        {
            return NotFound();
        }
        return this.mapper.Map<MedicamentoDto>(medicamento);
    }

    [HttpGet("GetInfoMedicamentoVendidos")]
    public async Task<IActionResult> GetInfoMedicamentoVendidos()
    {
        var Medicamento = await unitofwork.Medicamentos.GetInfoMedicamentoVendidos();
        if (Medicamento == null)
        {
            return NotFound(); // Devuelve 404 si no se encuentra el recurso.
        }
        return Ok(this.mapper.Map<IEnumerable<Object>>(Medicamento)); // Devuelve la colección si se encontró.
    }

    [HttpGet("GetInfoMedicamentoMenosVendido")]
    public async Task<IActionResult> GetInfoMedicamentoMenosVendido()
    {
        var Medicamento = await unitofwork.Medicamentos.GetInfoMedicamentoMenosVendido();
        if (Medicamento == null)
        {
            return NotFound(); // Devuelve 404 si no se encuentra el recurso.
        }
        return Ok(this.mapper.Map<IEnumerable<Object>>(Medicamento)); // Devuelve la colección si se encontró.
    }

    [HttpGet("GetInfoPromedioMedicamento")]
    public async Task<IActionResult> GetInfoPromedioMedicamento()
    {
        var Medicamento = await unitofwork.Medicamentos.GetInfoPromedioMedicamento();
        if (Medicamento == null)
        {
            return NotFound(); // Devuelve 404 si no se encuentra el recurso.
        }
        return Ok(this.mapper.Map<IEnumerable<Object>>(Medicamento)); // Devuelve la colección si se encontró.
    }

    [HttpGet("GetInfoMedicamentoVencidos")]
    public async Task<IActionResult> GetInfoMedicamentoVencidos()
    {
        var Medicamento = await unitofwork.Medicamentos.GetInfoMedicamentoVencidos();
        if (Medicamento == null)
        {
            return NotFound(); // Devuelve 404 si no se encuentra el recurso.
        }
        return Ok(this.mapper.Map<IEnumerable<Object>>(Medicamento)); // Devuelve la colección si se encontró.
    }

    [HttpGet("consulta31")]
    public async Task<IActionResult> MedicamentosVendidosPorMesEn2023Async()
    {
        var Medicamento = await unitofwork.Medicamentos.MedicamentosVendidosPorMesEn2023Async();
        if (Medicamento == null)
        {
            return NotFound(); // Devuelve 404 si no se encuentra el recurso.
        }
        return Ok(this.mapper.Map<IEnumerable<Object>>(Medicamento)); // Devuelve la colección si se encontró.
    }
    [HttpGet("consulta34/{year}")]
    public async Task<IActionResult> MedicamentosNoVendidosEn2023(int year)
    {
        var medicamentos = await unitofwork.Medicamentos.MedicamentosNoVendidosEn2023(year);

        if (medicamentos == null || !medicamentos.Any())
        {
            return NotFound(); // Devuelve 404 si no se encontraron medicamentos.
        }

        return Ok(this.mapper.Map<IEnumerable<object>>(medicamentos));
    }
    [HttpGet("consulta36")]
    public async Task<IActionResult> TotalMedicamentosVendidosPrimerTrimestre2023()
    {
        var totalMedicamentos = await unitofwork.Medicamentos.TotalMedicamentosVendidosPrimerTrimestre2023();

        if (totalMedicamentos == 0)
        {
            return NotFound(); // Devuelve 404 si el total es cero.
        }

        return Ok(totalMedicamentos); // Devuelve el total como un valor entero.
    }
    [HttpGet("consulta38")]
public async Task<IActionResult> MedicamentosConPrecioYStockAsync()
{
    var totalMedicamentos = await unitofwork.Medicamentos.MedicamentosConPrecioYStockAsync();

    if (totalMedicamentos == null || !totalMedicamentos.Any())
    {
        return NotFound(); // Devuelve 404 si la colección está vacía.
    }

    return Ok(totalMedicamentos); // Devuelve la colección si no está vacía.
}


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Medicamento>> Post(MedicamentoDto medicamentoDto)
    {
        var medicamento = this.mapper.Map<Medicamento>(medicamentoDto);
        this.unitofwork.Medicamentos.Add(medicamento);
        await unitofwork.SaveAsync();
        if (medicamento == null)
        {
            return BadRequest();
        }
        medicamentoDto.Id = medicamento.Id;
        return CreatedAtAction(nameof(Post), new { id = medicamentoDto.Id }, medicamentoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<MedicamentoDto>> Put(int id, [FromBody] MedicamentoDto medicamentoDto)
    {
        if (medicamentoDto == null)
        {
            return NotFound();
        }
        var medicamento = this.mapper.Map<Medicamento>(medicamentoDto);
        unitofwork.Medicamentos.Update(medicamento);
        await unitofwork.SaveAsync();
        return medicamentoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var medicamento = await unitofwork.Medicamentos.GetByIdAsync(id);
        if (medicamento == null)
        {
            return NotFound();
        }
        unitofwork.Medicamentos.Remove(medicamento);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    [HttpGet("MedicamentosNoVendidos")]

    public async Task<IEnumerable<object>> ObtenerMedicamentosNoVendidosAsync()
    {
        var medicamento = await unitofwork.Medicamentos.ObtenerMedicamentosNoVendidosAsync();
        return mapper.Map<IEnumerable<object>>(medicamento);
    }

    [HttpGet("totalmedicamentospormes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> ObtenerTotalMedicamentosVendidosPorMesEn2023Async()
    {
        var totalMedicamentosPorMes = await unitofwork.Medicamentos.ObtenerTotalMedicamentosVendidosPorMesEn2023Async();
        return Ok(totalMedicamentosPorMes);
    }


}