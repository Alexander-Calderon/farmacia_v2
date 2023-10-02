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

    // [HttpGet("{id}")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]

    // public async Task<ActionResult<EmpleadoDto>> Get111(int id)
    // {
    //     var Empleado = await unitofwork.Empleados.GetByIdAsync(id);
    //     if (Empleado == null)
    //     {
    //         return NotFound();
    //     }
    //     return this.mapper.Map<EmpleadoDto>(Empleado);
    // }

    [HttpGet("GetInfoCantidadVentas")]
    public async Task<IActionResult> GetInfoCantidadVentas()
    {
        var Empleado = await unitofwork.Empleados.GetInfoCantidadVentas();
        if (Empleado == null)
        {
            return NotFound(); // Devuelve 404 si no se encuentra el recurso.
        }
        return Ok(this.mapper.Map<IEnumerable<Object>>(Empleado)); // Devuelve la colección si se encontró.
    }

    [HttpGet("GetEmpleadocon5Ventas")]
    public async Task<IActionResult> GetEmpleadocon5Ventas()
    {
        var Empleado = await unitofwork.Empleados.GetEmpleadocon5Ventas();
        if (Empleado == null)
        {
            return NotFound(); // Devuelve 404 si no se encuentra el recurso.
        }
        return Ok(this.mapper.Map<IEnumerable<Object>>(Empleado)); // Devuelve la colección si se encontró.
    }
    
    [HttpGet("consulta37")]
    public async Task<IActionResult> EmpleadosSinVentasEnAbril2023Async()
    {
        var Empleado = await unitofwork.Empleados.EmpleadosSinVentasEnAbril2023Async();
        if (Empleado == null)
        {
            return NotFound(); // Devuelve 404 si no se encuentra el recurso.
        }
        return Ok(this.mapper.Map<IEnumerable<Object>>(Empleado)); // Devuelve la colección si se encontró.
    }
    /* [HttpPost]
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
    } */

    /* [HttpPut("{id}")]
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
    } */
}
