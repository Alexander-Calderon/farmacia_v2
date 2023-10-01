
using ApiFarmacia.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIFarmacia.Controllers;

    public class MarcaController : ApiBaseController
    {
        private readonly IUnitOfWork unitofwork;
        private readonly IMapper mapper;
    
        public MarcaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitofwork = unitOfWork;
            this.mapper = mapper;
        }
    
    
        [HttpGet]
        // [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<MarcaDto>>> Get()
        {
            var marca = await unitofwork.Marcas.GetAllAsync();
            return mapper.Map<List<MarcaDto>>(marca);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<MarcaDto>> Get(int id)
        {
            var marca = await unitofwork.Marcas.GetByIdAsync(id);
            if (marca == null){
                return NotFound();
            }
            return this.mapper.Map<MarcaDto>(marca);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Marca>> Post(MarcaDto marcaDto)
        {
            var marca = this.mapper.Map<Marca>(marcaDto);
            this.unitofwork.Marcas.Add(marca);
            await unitofwork.SaveAsync();
            if(marca == null)
            {
                return BadRequest();
            }
            marcaDto.Id = marca.Id;
            return CreatedAtAction(nameof(Post), new {id = marcaDto.Id}, marcaDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<MarcaDto>> Put(int id, [FromBody]MarcaDto marcaDto){
            if(marcaDto == null)
            {
                return NotFound();
            }
            var marca = this.mapper.Map<Marca>(marcaDto);
            unitofwork.Marcas.Update(marca);
            await unitofwork.SaveAsync();
            return marcaDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id){
            var marca = await unitofwork.Marcas.GetByIdAsync(id);
            if(marca == null)
            {
                return NotFound();
            }
            unitofwork.Marcas.Remove(marca);
            await unitofwork.SaveAsync();
            return NoContent();
        }
    } 