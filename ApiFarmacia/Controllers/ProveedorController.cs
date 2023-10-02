using ApiFarmacia.Dtos;
using APIFarmacia.Controllers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiFarmacia.Controllers;
public class ProveedorController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public ProveedorController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    }


    [HttpGet]
    // [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ProveedorDto>>> Get()
    {
        var Proveedor = await unitofwork.Proveedores.GetAllAsync();
        return mapper.Map<List<ProveedorDto>>(Proveedor);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ProveedorDto>> Get(int id)
    {
        var Proveedor = await unitofwork.Proveedores.GetByIdAsync(id);
        if (Proveedor == null)
        {
            return NotFound();
        }
        return this.mapper.Map<ProveedorDto>(Proveedor);
    }

    [HttpGet("GetInfoMedicamentoPorProveedor")]
    public async Task<IActionResult> GetInfoMedicamentoPorProveedor()
    {
        var Proveedor = await unitofwork.Proveedores.GetInfoMedicamentoPorProveedor();
        if (Proveedor == null)
        {
            return NotFound(); // Devuelve 404 si no se encuentra el recurso.
        }
        return Ok(this.mapper.Map<IEnumerable<Object>>(Proveedor)); // Devuelve la colección si se encontró.
    }

    [HttpGet("GetInfoVentaUltimoAnoProveedor")]
    public async Task<IActionResult> GetInfoVentaUltimoAnoProveedor()
    {
        var Proveedor = await unitofwork.Proveedores.GetInfoVentaUltimoAnoProveedor();
        if (Proveedor == null)
        {
            return NotFound(); // Devuelve 404 si no se encuentra el recurso.
        }
        return Ok(this.mapper.Map<IEnumerable<Object>>(Proveedor)); // Devuelve la colección si se encontró.
    }

<<<<<<< HEAD
=======
            [HttpGet("GetInfoGananciaPorProveedor")]
            public async Task<IActionResult> GetInfoGananciaPorProveedor()
            {
                var Proveedor = await unitofwork.Proveedores.GetInfoGananciaPorProveedor();
                if (Proveedor == null)
                {
                    return NotFound(); // Devuelve 404 si no se encuentra el recurso.
                }
                return Ok(this.mapper.Map<IEnumerable<Object>>(Proveedor)); // Devuelve la colección si se encontró.
            }

    
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
>>>>>>> a9760254e668227d181f52a65af81084078ccc70

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]


    public async Task<ActionResult<Proveedor>> Post(ProveedorDto proveedorDto)
    {
        var Proveedor = this.mapper.Map<Proveedor>(proveedorDto);
        this.unitofwork.Proveedores.Add(Proveedor);
        await unitofwork.SaveAsync();
        if (Proveedor == null)
        {
            return BadRequest();
        }
        proveedorDto.Id = Proveedor.Id;
        return CreatedAtAction(nameof(Post), new { id = proveedorDto.Id }, proveedorDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ProveedorDto>> Put(int id, [FromBody] ProveedorDto proveedorDto)
    {
        if (proveedorDto == null)
        {
            return NotFound();
        }
        var Proveedor = this.mapper.Map<Proveedor>(proveedorDto);
        unitofwork.Proveedores.Update(Proveedor);
        await unitofwork.SaveAsync();
        return proveedorDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var Proveedor = await unitofwork.Proveedores.GetByIdAsync(id);
        if (Proveedor == null)
        {
            return NotFound();
        }
        unitofwork.Proveedores.Remove(Proveedor);
        await unitofwork.SaveAsync();
        return NoContent();
    }

    [HttpGet("proveedorconmasmedicamentossuministrados")]
    public async Task<ActionResult<object>> ObtenerProveedorConMasMedicamentosSuministradosAsync()
    {
        var proveedorConMasMedicamentosSuministrados = await unitofwork.Proveedores.ObtenerProveedorConMasMedicamentosSuministradosAsync();
        return Ok(proveedorConMasMedicamentosSuministrados);
    }

    [HttpGet("ObtenerTotalProveedoressuminitro")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> ObtenerTotalProveedoressuminitroAsync()
    {
        var totalProveedores = await unitofwork.Proveedores.ObtenerTotalProveedoressuminitroAsync();
        return Ok(totalProveedores);
    }

    [HttpGet("proveedoresConStockBajo/{stockMinimo}")]
    public async Task<ActionResult<IEnumerable<object>>> ObtenerProveedoresConStockBajoAsync(int stockMinimo)
    {
        var proveedores = await unitofwork.Proveedores.ObtenerProveedoresConStockBajoAsync(stockMinimo);
        return Ok(proveedores);
    }



}