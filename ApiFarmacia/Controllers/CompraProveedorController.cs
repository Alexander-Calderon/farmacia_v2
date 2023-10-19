using ApiFarmacia.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIFarmacia.Controllers;

    public class CompraProveedorController : ApiBaseController
    {
        private readonly IUnitOfWork unitofwork;
        private readonly IMapper mapper;
    
        public CompraProveedorController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitofwork = unitOfWork;
            this.mapper = mapper;
        }
    
    
        [HttpGet]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<CompraProveedorDto>>> Get()
        {
            var compraproveedor = await unitofwork.CompraProveedores.GetAllAsync();
            return mapper.Map<List<CompraProveedorDto>>(compraproveedor);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<CompraProveedorDto>> Get(int id)
        {
            var compraproveedor = await unitofwork.CompraProveedores.GetByIdAsync(id);
            if (compraproveedor == null){
                return NotFound();
            }
            return this.mapper.Map<CompraProveedorDto>(compraproveedor);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<CompraProveedor>> Post(CompraProveedorDto compraProveedorDto)
        {
            var compraproveedor = this.mapper.Map<CompraProveedor>(compraProveedorDto);
            this.unitofwork.CompraProveedores.Add(compraproveedor);
            await unitofwork.SaveAsync();
            if(compraproveedor == null)
            {
                return BadRequest();
            }
            compraProveedorDto.Id = compraproveedor.Id;
            return CreatedAtAction(nameof(Post), new {id = compraProveedorDto.Id}, compraProveedorDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<CompraProveedorDto>> Put(int id, [FromBody]CompraProveedorDto compraProveedorDto){
            if(compraProveedorDto == null)
            {
                return NotFound();
            }
            var compraproveedor = this.mapper.Map<CompraProveedor>(compraProveedorDto);
            unitofwork.CompraProveedores.Update(compraproveedor);
            await unitofwork.SaveAsync();
            return compraProveedorDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id){
            var compraproveedor = await unitofwork.CompraProveedores.GetByIdAsync(id);
            if(compraproveedor == null)
            {
                return NotFound();
            }
            unitofwork.CompraProveedores.Remove(compraproveedor);
            await unitofwork.SaveAsync();
            return NoContent();
        }
    } 