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

    [HttpGet("totalMedicamentosVendidosPorProveedor")]
    public async Task<IActionResult> TotalMedicamentosVendidosPorProveedorAsync()
    {
        var totalMedicamentosPorProveedor = await unitofwork.Medicamentos.TotalMedicamentosVendidosPorProveedorAsync();

        if (totalMedicamentosPorProveedor == null || !totalMedicamentosPorProveedor.Any())
        {
            return NotFound(); 
        }

        return Ok(totalMedicamentosPorProveedor); 
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


    // Enpoints Requeridos:

    // * 1 Obtener todos los medicamentos con menos de 50 unidades en stock.
    [HttpGet("StockMenorA50")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MedicamentoDto>>> GetStockMenorA50()
    {
        var medicamentos = await unitofwork.Medicamentos.GetCantidadMenorA50(); //Redirecciono al método que devuelve lo solicitado en este EndPoint.
        var medicamentoDtos = mapper.Map<List<MedicamentoDto>>(medicamentos); // luego los datos completos del método les aplico un filtro usando un Dto para elegir que datos voy a mostrar.
        return Ok(medicamentoDtos); 
    }

    [HttpGet("medicamentosNoVendidos")]
    public async Task<IActionResult> MedicamentosNoVendidosAsync()
    {
        var medicamentosNoVendidos = await unitofwork.Medicamentos.MedicamentosNoVendidosAsync();

        if (medicamentosNoVendidos == null || !medicamentosNoVendidos.Any())
        {
            return NotFound(); 
        }

        return Ok(medicamentosNoVendidos);
    }


        

    [HttpGet("totalmedicamentospormes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> ObtenerTotalMedicamentosVendidosPorMesEn2023Async()
    {
        var totalMedicamentosPorMes = await unitofwork.Medicamentos.ObtenerTotalMedicamentosVendidosPorMesEn2023Async();
        return Ok(totalMedicamentosPorMes);
    }





    [HttpGet("totalVentasParacetamol")]
    public async Task<IActionResult> TotalVentasParacetamolAsync()
    {
        var totalVentas = await unitofwork.Medicamentos.TotalVentasParacetamolAsync();

        var resultado = new { totalVentas };

        return Ok(resultado);
    }

    [HttpGet("medicamentosVencidosAntesDe1Enero")]
    public async Task<IActionResult> MedicamentosVencidosAntesDe1Enero()
    {
        var medicamentosCaducados = await unitofwork.Medicamentos.MedicamentosVencidosAntesDe1Enero();

        if (medicamentosCaducados == null || !medicamentosCaducados.Any())
        {
            return NotFound();
        }

        return Ok(medicamentosCaducados); 
    }

    [HttpGet("medicamentoMasCaro")]
    public async Task<IActionResult> ObtenerMedicamentoMasCaroAsync()
    {
        var medicamentoMasCaro = await unitofwork.Medicamentos.ObtenerMedicamentoMasCaroAsync();

        if (medicamentoMasCaro == null)
        {
            return NotFound();
        }

        return Ok(medicamentoMasCaro);
    }

    [HttpGet("totalMedicamentosVendidosMarzo2023")]
    public async Task<IActionResult> ObtenerTotalMedicamentosVendidosMarzo2023Async()
    {
        var totalMedicamentosVendidos = await unitofwork.Medicamentos.ObtenerTotalMedicamentosVendidosMarzo2023Async();

        return Ok(new { total_medicamentos_vendidos = totalMedicamentosVendidos });
    }

    
    [HttpGet("medicamentoMenosVendido2023")]
    public async Task<IActionResult> ObtenerMedicamentoMenosVendido2023Async()
    {
        var medicamentoMenosVendido = await unitofwork.Medicamentos.ObtenerMedicamentoMenosVendido2023Async();

        if (medicamentoMenosVendido == null)
        {
            return NotFound();
        }

        return Ok(new { medicamento_menos_vendido = medicamentoMenosVendido });
    }

            
        [HttpGet("promedioMedicamentosPorVenta")]
        public async Task<IActionResult> ObtenerPromedioMedicamentosPorVentaAsync()
        {
            var promedioMedicamentosPorVenta = await unitofwork.Medicamentos.ObtenerPromedioMedicamentosPorVentaAsync();

            return Ok(new { PromedioMedicamentosPorVenta = promedioMedicamentosPorVenta });
        }

        
        [HttpGet("medicamentosExpiranEn2024")]
        public async Task<IActionResult> ObtenerMedicamentosExpiranEn2024Async()
        {
            var medicamentosExpiranEn2024 = await  unitofwork.Medicamentos.ObtenerMedicamentosExpiranEn2024Async();

            return Ok(medicamentosExpiranEn2024);
        }



}