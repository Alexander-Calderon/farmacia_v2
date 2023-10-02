using ApiFarmacia.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIFarmacia.Controllers;

public class MedicamentoController : ApiBaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MedicamentoController(IUnitOfWork _unitOfWork, IMapper _mapper)
    {
        this._unitOfWork = _unitOfWork;
        this._mapper = _mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<MedicamentoDto>>> Get()
    {
        var medicamento = await _unitOfWork.Medicamentos.GetAllAsync();
        return _mapper.Map<List<MedicamentoDto>>(medicamento);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<MedicamentoDto>> Get(int id)
    {
        var medicamento = await _unitOfWork.Medicamentos.GetByIdAsync(id);
        if (medicamento == null)
        {
            return NotFound();
        }
        return this._mapper.Map<MedicamentoDto>(medicamento);
    }

    // [HttpGet("GetInfoMedicamentoPorProveedor")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]

    // public async Task<ActionResult<MedicamentoDto>> GetInfoMedicamentoPorProveedor()
    // {
    //     var medicamento = await _unitOfWork.Medicamentos.GetInfoMedicamentoPorProveedor();
    //     if (medicamento == null)
    //     {
    //         return NotFound();
    //     }
    //     return this._mapper.Map<MedicamentoDto>(medicamento);
    // }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Medicamento>> Post(MedicamentoDto medicamentoDto)
    {
        var medicamento = this._mapper.Map<Medicamento>(medicamentoDto);
        this._unitOfWork.Medicamentos.Add(medicamento);
        await _unitOfWork.SaveAsync();
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
        var medicamento = this._mapper.Map<Medicamento>(medicamentoDto);
        _unitOfWork.Medicamentos.Update(medicamento);
        await _unitOfWork.SaveAsync();
        return medicamentoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> Delete(int id)
    {
        var medicamento = await _unitOfWork.Medicamentos.GetByIdAsync(id);
        if (medicamento == null)
        {
            return NotFound();
        }
        _unitOfWork.Medicamentos.Remove(medicamento);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    // Enpoints Requeridos:

    // * 1 Obtener todos los medicamentos con menos de 50 unidades en stock.
    [HttpGet("StockMenorA50")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MedicamentoDto>>> GetStockMenorA50()
    {
        var medicamentos = await _unitOfWork.Medicamentos.GetCantidadMenorA50(); //Redirecciono al método que devuelve lo solicitado en este EndPoint.
        var medicamentoDtos = _mapper.Map<List<MedicamentoDto>>(medicamentos); // luego los datos completos del método les aplico un filtro usando un Dto para elegir que datos voy a mostrar.
        return Ok(medicamentoDtos); 
    }


        










}