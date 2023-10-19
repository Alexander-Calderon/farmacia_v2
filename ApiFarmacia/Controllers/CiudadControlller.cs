
using ApiFarmacia.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIFarmacia.Controllers;

    public class CiudadController : ApiBaseController
    {
        private readonly IUnitOfWork unitofwork;
        private readonly IMapper mapper;
    
        public CiudadController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitofwork = unitOfWork;
            this.mapper = mapper;
        }
    
    
        [HttpGet]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<CiudadDto>>> Get()
        {
            var ciudad = await unitofwork.Cuidades.GetAllAsync();
            return mapper.Map<List<CiudadDto>>(ciudad);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<CiudadDto>> Get(int id)
        {
            var ciudad = await unitofwork.Cuidades.GetByIdAsync(id);
            if (ciudad == null){
                return NotFound();
            }
            return this.mapper.Map<CiudadDto>(ciudad);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Ciudad>> Post(CiudadDto ciudadDto)
        {
            var ciudad = this.mapper.Map<Ciudad>(ciudadDto);
            this.unitofwork.Cuidades.Add(ciudad);
            await unitofwork.SaveAsync();
            if(ciudad == null)
            {
                return BadRequest();
            }
            ciudadDto.Id = ciudad.Id;
            return CreatedAtAction(nameof(Post), new {id = ciudadDto.Id}, ciudadDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<CiudadDto>> Put(int id, [FromBody]CiudadDto ciudadDto){
            if(ciudadDto == null)
            {
                return NotFound();
            }
            var ciudad = this.mapper.Map<Ciudad>(ciudadDto);
            unitofwork.Cuidades.Update(ciudad);
            await unitofwork.SaveAsync();
            return ciudadDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id){
            var ciudad = await unitofwork.Cuidades.GetByIdAsync(id);
            if(ciudad == null)
            {
                return NotFound();
            }
            unitofwork.Cuidades.Remove(ciudad);
            await unitofwork.SaveAsync();
            return NoContent();
        }
    } 