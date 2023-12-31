using ApiFarmacia.Dtos;
using APIFarmacia.Controllers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiFarmacia.Controllers;
public class EmpleadoController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public EmpleadoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    }


    [HttpGet]

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<EmpleadoDto>>> Get()
    {
        var Empleado = await unitofwork.Empleados.GetAllAsync();
        return mapper.Map<List<EmpleadoDto>>(Empleado);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<EmpleadoDto>> Get(int id)
    {
        var Empleado = await unitofwork.Empleados.GetByIdAsync(id);
        if (Empleado == null)
        {
            return NotFound();
        }
        return this.mapper.Map<EmpleadoDto>(Empleado);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Empleado>> Post(EmpleadoDto empleadoDto)
    {
        var Empleado = this.mapper.Map<Empleado>(empleadoDto);
        this.unitofwork.Empleados.Add(Empleado);
        await unitofwork.SaveAsync();
        if (Empleado == null)
        {
            return BadRequest();
        }
        empleadoDto.Id = Empleado.Id;
        return CreatedAtAction(nameof(Post), new { id = empleadoDto.Id }, empleadoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<EmpleadoDto>> Put(int id, [FromBody] EmpleadoDto empleadoDto)
    {
        if (empleadoDto == null)
        {
            return NotFound();
        }
        var Empleado = this.mapper.Map<Empleado>(empleadoDto);
        unitofwork.Empleados.Update(Empleado);
        await unitofwork.SaveAsync();
        return empleadoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var Empleado = await unitofwork.Empleados.GetByIdAsync(id);
        if (Empleado == null)
        {
            return NotFound();
        }
        unitofwork.Empleados.Remove(Empleado);
        await unitofwork.SaveAsync();
        return NoContent();
    }


    [HttpGet("empleadossinfacturas")]

    public async Task<ActionResult<IEnumerable<Empleado>>> ObtenerEmpleadosSinFacturasAsync()
    {
        var empleadosSinFacturas = await unitofwork.Empleados.ObtenerEmpleadosSinFacturasAsync();
        return Ok(empleadosSinFacturas);
    }

    [HttpGet("empleadosconmenosde5")]

    public async Task<ActionResult<IEnumerable<EmpleadoDto>>> Get11()
    {
        var Empleado = await unitofwork.Empleados.GetAllAsync();
        return mapper.Map<List<EmpleadoDto>>(Empleado);
    }

    [HttpGet("cantidadVentasPorEmpleado")]
    public async Task<IActionResult> ObtenerCantidadVentasPorEmpleadoAsync()
    {
        var ventasPorEmpleado = await unitofwork.Empleados.ObtenerCantidadVentasPorEmpleadoAsync();

        return Ok(ventasPorEmpleado);
    }

    

    [HttpGet("Empleadocon5Ventas")]
    public async Task<IActionResult> GetEmpleadocon5Ventas()
    {
        var Empleado = await unitofwork.Empleados.GetEmpleadocon5Ventas();
        if (Empleado == null)
        {
            return NotFound(); 
        }
        return Ok(this.mapper.Map<IEnumerable<Object>>(Empleado)); 
    }
    
    [HttpGet("EmpleadosSinVentasEnAbril2023")]
    public async Task<IActionResult> EmpleadosSinVentasEnAbril2023Async()
    {
        var Empleado = await unitofwork.Empleados.EmpleadosSinVentasEnAbril2023Async();
        if (Empleado == null)
        {
            return NotFound(); 
        }
        return Ok(this.mapper.Map<IEnumerable<Object>>(Empleado)); 
    }

    
    [HttpGet("empleadosConMasDe5Ventas")]
    public async Task<IActionResult> ObtenerEmpleadosConMasDe5VentasAsync()
    {
        var empleadosConMasDe5Ventas = await unitofwork.Empleados.ObtenerEmpleadosConMasDe5VentasAsync();

        return Ok(empleadosConMasDe5Ventas);
    }
    
}
