    using ApiFarmacia.Dtos;
using APIFarmacia.Controllers;
using AutoMapper;
    using Domain.Entities;
    using Domain.Interfaces;
    using Microsoft.AspNetCore.Mvc;


namespace ApiFarmacia.Controllers;
    public class DireccionController : ApiBaseController
        {
            private readonly IUnitOfWork unitofwork;
            private readonly IMapper mapper;
        
            public DireccionController(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this.unitofwork = unitOfWork;
                this.mapper = mapper;
            }
        
        
            [HttpGet]
            
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
            public async Task<ActionResult<IEnumerable<DireccionDto>>> Get()
            {
                var Direccion = await unitofwork.Direcciones.GetAllAsync();
                return mapper.Map<List<DireccionDto>>(Direccion);
            }
    
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<ActionResult<DireccionDto>> Get(int id)
            {
                var Direccion = await unitofwork.Direcciones.GetByIdAsync(id);
                if (Direccion == null){
                    return NotFound();
                }
                return this.mapper.Map<DireccionDto>(Direccion);
            }
    
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
            public async Task<ActionResult<Direccion>> Post(DireccionDto direccionDto)
            {
                var Direccion = this.mapper.Map<Direccion>(direccionDto);
                this.unitofwork.Direcciones.Add(Direccion);
                await unitofwork.SaveAsync();
                if(Direccion == null)
                {
                    return BadRequest();
                }
                direccionDto.Id = Direccion.Id;
                return CreatedAtAction(nameof(Post), new {id = direccionDto.Id}, direccionDto);
            }
            
            [HttpPut("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<ActionResult<DireccionDto>> Put(int id, [FromBody]DireccionDto direccionDto){
                if(direccionDto == null)
                {
                    return NotFound();
                }
                var Direccion = this.mapper.Map<Direccion>(direccionDto);
                unitofwork.Direcciones.Update(Direccion);
                await unitofwork.SaveAsync();
                return direccionDto;
            }
    
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<IActionResult> Delete(int id){
                var Direccion = await unitofwork.Direcciones.GetByIdAsync(id);
                if(Direccion == null)
                {
                    return NotFound();
                }
                unitofwork.Direcciones.Remove(Direccion);
                await unitofwork.SaveAsync();
                return NoContent();
            }
        } 