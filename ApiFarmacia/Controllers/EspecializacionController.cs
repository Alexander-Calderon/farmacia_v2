
    using ApiFarmacia.Dtos;
using APIFarmacia.Controllers;
using AutoMapper;
    using Domain.Entities;
    using Domain.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    

namespace ApiFarmacia.Controllers;
    public class EspecializacionController: ApiBaseController
        {
            private readonly IUnitOfWork unitofwork;
            private readonly IMapper mapper;
        
            public EspecializacionController(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this.unitofwork = unitOfWork;
                this.mapper = mapper;
            }
        
        
            [HttpGet]
            // [MapToApiVersion("1.0")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
            public async Task<ActionResult<IEnumerable<EspecializacionDto>>> Get()
            {
                var Especializacion = await unitofwork.Especializacion.GetAllAsync();
                return mapper.Map<List<EspecializacionDto>>(Especializacion);
            }
    
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<ActionResult<EspecializacionDto>> Get(int id)
            {
                var Especializacion = await unitofwork.Especializacion.GetByIdAsync(id);
                if (Especializacion == null){
                    return NotFound();
                }
                return this.mapper.Map<EspecializacionDto>(Especializacion);
            }
    
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
            public async Task<ActionResult<Especializacion>> Post(EspecializacionDto especializacionDto)
            {
                var Especializacion = this.mapper.Map<Especializacion>(especializacionDto);
                this.unitofwork.Especializacion.Add(Especializacion);
                await unitofwork.SaveAsync();
                if(Especializacion == null)
                {
                    return BadRequest();
                }
                especializacionDto.Id = Especializacion.Id;
                return CreatedAtAction(nameof(Post), new {id = especializacionDto.Id}, especializacionDto);
            }
            
            [HttpPut("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<ActionResult<EspecializacionDto>> Put(int id, [FromBody]EspecializacionDto especializacionDto){
                if(especializacionDto == null)
                {
                    return NotFound();
                }
                var Especializacion = this.mapper.Map<Especializacion>(especializacionDto);
                unitofwork.Especializacion.Update(Especializacion);
                await unitofwork.SaveAsync();
                return especializacionDto;
            }
    
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<IActionResult> Delete(int id){
                var Especializacion = await unitofwork.Especializacion.GetByIdAsync(id);
                if(Especializacion == null)
                {
                    return NotFound();
                }
                unitofwork.Especializacion.Remove(Especializacion);
                await unitofwork.SaveAsync();
                return NoContent();
            }
        } 