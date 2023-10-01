using ApiFarmacia.Dtos;
using APIFarmacia.Controllers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace ApiFarmacia.Controllers;
public class DetalleFacturaController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public DetalleFacturaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitofwork = unitOfWork;
        this.mapper = mapper;
    }


    [HttpGet]
    // [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<DetalleFacturaDto>>> Get()
    {
        var DetalleFactura = await unitofwork.DetalleFacturas.GetAllAsync();
        return mapper.Map<List<DetalleFacturaDto>>(DetalleFactura);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<DetalleFacturaDto>> Get(int id)
    {
        var DetalleFactura = await unitofwork.DetalleFacturas.GetByIdAsync(id);
        if (DetalleFactura == null)
        {
            return NotFound();
        }
        return this.mapper.Map<DetalleFacturaDto>(DetalleFactura);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<DetalleFactura>> Post(DetalleFacturaDto detallerFacturaDto)
    {
        var DetalleFactura = this.mapper.Map<DetalleFactura>(detallerFacturaDto);
        this.unitofwork.DetalleFacturas.Add(DetalleFactura);
        await unitofwork.SaveAsync();
        if (DetalleFactura == null)
        {
            return BadRequest();
        }
        detallerFacturaDto.Id = DetalleFactura.Id;
        return CreatedAtAction(nameof(Post), new { id = detallerFacturaDto.Id }, detallerFacturaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<DetalleFacturaDto>> Put(int id, [FromBody] DetalleFacturaDto detallerFacturaDto)
    {
        if (detallerFacturaDto == null)
        {
            return NotFound();
        }
        var DetalleFactura = this.mapper.Map<DetalleFactura>(detallerFacturaDto);
        unitofwork.DetalleFacturas.Update(DetalleFactura);
        await unitofwork.SaveAsync();
        return detallerFacturaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var DetalleFactura = await unitofwork.DetalleFacturas.GetByIdAsync(id);
        if (DetalleFactura == null)
        {
            return NotFound();
        }
        unitofwork.DetalleFacturas.Remove(DetalleFactura);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}