using ApiFarmacia.Dtos;
using APIFarmacia.Controllers;
using AutoMapper;
    using Domain.Entities;
    using Domain.Interfaces;
    using Microsoft.AspNetCore.Mvc;

namespace ApiFarmacia.Controllers;
    public class ContactoController : ApiBaseController
        {
            private readonly IUnitOfWork unitofwork;
            private readonly IMapper mapper;
        
            public ContactoController(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this.unitofwork = unitOfWork;
                this.mapper = mapper;
            }
        
        
            [HttpGet]
            
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
            public async Task<ActionResult<IEnumerable<ContactoDto>>> Get()
            {
                var contacto = await unitofwork.Contactos.GetAllAsync();
                return mapper.Map<List<ContactoDto>>(contacto);
            }
    
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<ActionResult<ContactoDto>> Get(int id)
            {
                var contacto = await unitofwork.Contactos.GetByIdAsync(id);
                if (contacto == null){
                    return NotFound();
                }
                return this.mapper.Map<ContactoDto>(contacto);
            }
    
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
            public async Task<ActionResult<Contacto>> Post(ContactoDto contactoDto)
            {
                var Contacto = this.mapper.Map<Contacto>(contactoDto);
                this.unitofwork.Contactos.Add(Contacto);
                await unitofwork.SaveAsync();
                if(Contacto == null)
                {
                    return BadRequest();
                }
                contactoDto.Id = Contacto.Id;
                return CreatedAtAction(nameof(Post), new {id = contactoDto.Id}, contactoDto);
            }
            
            [HttpPut("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<ActionResult<ContactoDto>> Put(int id, [FromBody]ContactoDto contactoDto){
                if(contactoDto == null)
                {
                    return NotFound();
                }
                var Contacto = this.mapper.Map<Contacto>(contactoDto);
                unitofwork.Contactos.Update(Contacto);
                await unitofwork.SaveAsync();
                return contactoDto;
            }
    
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<IActionResult> Delete(int id){
                var Contacto = await unitofwork.Contactos.GetByIdAsync(id);
                if(Contacto == null)
                {
                    return NotFound();
                }
                unitofwork.Contactos.Remove(Contacto);
                await unitofwork.SaveAsync();
                return NoContent();
            }
        } 