
using ApiFarmacia.Dtos;
using APIFarmacia.Controllers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace ApiFarmacia.Controllers;
    public class CargoController: ApiBaseController
        {
            private readonly IUnitOfWork unitofwork;
            private readonly IMapper mapper;
        
            public CargoController(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this.unitofwork = unitOfWork;
                this.mapper = mapper;
            }
        
        
            [HttpGet]
            // [MapToApiVersion("1.0")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
            public async Task<ActionResult<IEnumerable<CargoDto>>> Get()
            {
                var cargo = await unitofwork.Cargos.GetAllAsync();
                return mapper.Map<List<CargoDto>>(cargo);
            }
    
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<ActionResult<CargoDto>> Get(int id)
            {
                var cargo = await unitofwork.Cargos.GetByIdAsync(id);
                if (cargo == null){
                    return NotFound();
                }
                return this.mapper.Map<CargoDto>(cargo);
            }
    
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
            public async Task<ActionResult<Cargo>> Post(CargoDto marcaDto)
            {
                var cargo = this.mapper.Map<Cargo>(marcaDto);
                this.unitofwork.Cargos.Add(cargo);
                await unitofwork.SaveAsync();
                if(cargo == null)
                {
                    return BadRequest();
                }
                marcaDto.Id = cargo.Id;
                return CreatedAtAction(nameof(Post), new {id = marcaDto.Id}, marcaDto);
            }
            
            [HttpPut("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<ActionResult<CargoDto>> Put(int id, [FromBody]CargoDto marcaDto){
                if(marcaDto == null)
                {
                    return NotFound();
                }
                var cargo = this.mapper.Map<Cargo>(marcaDto);
                unitofwork.Cargos.Update(cargo);
                await unitofwork.SaveAsync();
                return marcaDto;
            }
    
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<IActionResult> Delete(int id){
                var cargo = await unitofwork.Cargos.GetByIdAsync(id);
                if(cargo == null)
                {
                    return NotFound();
                }
                unitofwork.Cargos.Remove(cargo);
                await unitofwork.SaveAsync();
                return NoContent();
            }
        } 
