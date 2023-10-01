    using ApiFarmacia.Dtos;
using APIFarmacia.Controllers;
using AutoMapper;
    using Domain.Entities;
    using Domain.Interfaces;
    using Microsoft.AspNetCore.Mvc;

namespace ApiFarmacia.Controllers;
    public class ProveedorController : ApiBaseController
        {
            private readonly IUnitOfWork unitofwork;
            private readonly IMapper mapper;
        
            public ProveedorController(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this.unitofwork = unitOfWork;
                this.mapper = mapper;
            }
        
        
            [HttpGet]
            // [MapToApiVersion("1.0")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
            public async Task<ActionResult<IEnumerable<ProveedorDto>>> Get()
            {
                var Proveedor = await unitofwork.Proveedores.GetAllAsync();
                return mapper.Map<List<ProveedorDto>>(Proveedor);
            }
    
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<ActionResult<ProveedorDto>> Get(int id)
            {
                var Proveedor = await unitofwork.Proveedores.GetByIdAsync(id);
                if (Proveedor == null){
                    return NotFound();
                }
                return this.mapper.Map<ProveedorDto>(Proveedor);
            }
    
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
            public async Task<ActionResult<Proveedor>> Post(ProveedorDto proveedorDto)
            {
                var Proveedor = this.mapper.Map<Proveedor>(proveedorDto);
                this.unitofwork.Proveedores.Add(Proveedor);
                await unitofwork.SaveAsync();
                if(Proveedor == null)
                {
                    return BadRequest();
                }
                proveedorDto.Id = Proveedor.Id;
                return CreatedAtAction(nameof(Post), new {id = proveedorDto.Id}, proveedorDto);
            }
            
            [HttpPut("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<ActionResult<ProveedorDto>> Put(int id, [FromBody]ProveedorDto proveedorDto){
                if(proveedorDto == null)
                {
                    return NotFound();
                }
                var Proveedor = this.mapper.Map<Proveedor>(proveedorDto);
                unitofwork.Proveedores.Update(Proveedor);
                await unitofwork.SaveAsync();
                return proveedorDto;
            }
    
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<IActionResult> Delete(int id){
                var Proveedor = await unitofwork.Proveedores.GetByIdAsync(id);
                if(Proveedor == null)
                {
                    return NotFound();
                }
                unitofwork.Proveedores.Remove(Proveedor);
                await unitofwork.SaveAsync();
                return NoContent();
            }
        } 