
    using ApiFarmacia.Dtos;
using APIFarmacia.Controllers;
using AutoMapper;
    using Domain.Entities;
    using Domain.Interfaces;
    using Microsoft.AspNetCore.Mvc;

namespace ApiFarmacia.Controllers;
    public class TipoPresentacionController : ApiBaseController
        {
            private readonly IUnitOfWork unitofwork;
            private readonly IMapper mapper;
        
            public TipoPresentacionController(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this.unitofwork = unitOfWork;
                this.mapper = mapper;
            }
        
        
            [HttpGet]
            
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
            public async Task<ActionResult<IEnumerable<TipoPresentacionDto>>> Get()
            {
                var TipoPresentacion = await unitofwork.TipoPresentaciones.GetAllAsync();
                return mapper.Map<List<TipoPresentacionDto>>(TipoPresentacion);
            }
    
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<ActionResult<TipoPresentacionDto>> Get(int id)
            {
                var TipoPresentacion = await unitofwork.TipoPresentaciones.GetByIdAsync(id);
                if (TipoPresentacion == null){
                    return NotFound();
                }
                return this.mapper.Map<TipoPresentacionDto>(TipoPresentacion);
            }
    
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
            public async Task<ActionResult<TipoPresentacion>> Post(TipoPresentacionDto tipoPresentacionDto)
            {
                var TipoPresentacion = this.mapper.Map<TipoPresentacion>(tipoPresentacionDto);
                this.unitofwork.TipoPresentaciones.Add(TipoPresentacion);
                await unitofwork.SaveAsync();
                if(TipoPresentacion == null)
                {
                    return BadRequest();
                }
                tipoPresentacionDto.Id = TipoPresentacion.Id;
                return CreatedAtAction(nameof(Post), new {id = tipoPresentacionDto.Id}, tipoPresentacionDto);
            }
            
            [HttpPut("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<ActionResult<TipoPresentacionDto>> Put(int id, [FromBody]TipoPresentacionDto tipoPresentacionDto){
                if(tipoPresentacionDto == null)
                {
                    return NotFound();
                }
                var TipoPresentacion = this.mapper.Map<TipoPresentacion>(tipoPresentacionDto);
                unitofwork.TipoPresentaciones.Update(TipoPresentacion);
                await unitofwork.SaveAsync();
                return tipoPresentacionDto;
            }
    
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<IActionResult> Delete(int id){
                var TipoPresentacion = await unitofwork.TipoPresentaciones.GetByIdAsync(id);
                if(TipoPresentacion == null)
                {
                    return NotFound();
                }
                unitofwork.TipoPresentaciones.Remove(TipoPresentacion);
                await unitofwork.SaveAsync();
                return NoContent();
            }
        } 