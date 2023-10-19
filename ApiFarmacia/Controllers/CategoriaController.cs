using ApiFarmacia.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIFarmacia.Controllers;

    public class CategoriaController : ApiBaseController
    {
        private readonly IUnitOfWork unitofwork;
        private readonly IMapper mapper;
    
        public CategoriaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitofwork = unitOfWork;
            this.mapper = mapper;
        }
    
    
        [HttpGet]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<CategoriaDto>>> Get()
        {
            var categoria = await unitofwork.Categorias.GetAllAsync();
            return mapper.Map<List<CategoriaDto>>(categoria);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<CategoriaDto>> Get(int id)
        {
            var categoria = await unitofwork.Categorias.GetByIdAsync(id);
            if (categoria == null){
                return NotFound();
            }
            return this.mapper.Map<CategoriaDto>(categoria);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Categoria>> Post(CategoriaDto categoriaDto)
        {
            var categoria = this.mapper.Map<Categoria>(categoriaDto);
            this.unitofwork.Categorias.Add(categoria);
            await unitofwork.SaveAsync();
            if(categoria == null)
            {
                return BadRequest();
            }
            categoriaDto.Id = categoria.Id;
            return CreatedAtAction(nameof(Post), new {id = categoriaDto.Id}, categoriaDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<CategoriaDto>> Put(int id, [FromBody]CategoriaDto categoriaDto){
            if(categoriaDto == null)
            {
                return NotFound();
            }
            var categoria = this.mapper.Map<Categoria>(categoriaDto);
            unitofwork.Categorias.Update(categoria);
            await unitofwork.SaveAsync();
            return categoriaDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id){
            var categoria = await unitofwork.Categorias.GetByIdAsync(id);
            if(categoria == null)
            {
                return NotFound();
            }
            unitofwork.Categorias.Remove(categoria);
            await unitofwork.SaveAsync();
            return NoContent();
        }
    } 