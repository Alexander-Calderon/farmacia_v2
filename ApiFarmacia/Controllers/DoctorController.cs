    using ApiFarmacia.Dtos;
using APIFarmacia.Controllers;
using AutoMapper;
    using Domain.Entities;
    using Domain.Interfaces;
    using Microsoft.AspNetCore.Mvc;


namespace ApiFarmacia.Controllers;
    public class DoctorController : ApiBaseController
        {
            private readonly IUnitOfWork unitofwork;
            private readonly IMapper mapper;
        
            public DoctorController(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this.unitofwork = unitOfWork;
                this.mapper = mapper;
            }
        
        
            [HttpGet]
            // [MapToApiVersion("1.0")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
            public async Task<ActionResult<IEnumerable<DoctorDto>>> Get()
            {
                var Doctor = await unitofwork.Doctores.GetAllAsync();
                return mapper.Map<List<DoctorDto>>(Doctor);
            }
    
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<ActionResult<DoctorDto>> Get(int id)
            {
                var Doctor = await unitofwork.Doctores.GetByIdAsync(id);
                if (Doctor == null){
                    return NotFound();
                }
                return this.mapper.Map<DoctorDto>(Doctor);
            }
    
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
            public async Task<ActionResult<Doctor>> Post(DoctorDto doctorDto)
            {
                var Doctor = this.mapper.Map<Doctor>(doctorDto);
                this.unitofwork.Doctores.Add(Doctor);
                await unitofwork.SaveAsync();
                if(Doctor == null)
                {
                    return BadRequest();
                }
                doctorDto.Id = Doctor.Id;
                return CreatedAtAction(nameof(Post), new {id = doctorDto.Id}, doctorDto);
            }
            
            [HttpPut("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<ActionResult<DoctorDto>> Put(int id, [FromBody]DoctorDto doctorDto){
                if(doctorDto == null)
                {
                    return NotFound();
                }
                var Doctor = this.mapper.Map<Doctor>(doctorDto);
                unitofwork.Doctores.Update(Doctor);
                await unitofwork.SaveAsync();
                return doctorDto;
            }
    
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
    
            public async Task<IActionResult> Delete(int id){
                var Doctor = await unitofwork.Doctores.GetByIdAsync(id);
                if(Doctor == null)
                {
                    return NotFound();
                }
                unitofwork.Doctores.Remove(Doctor);
                await unitofwork.SaveAsync();
                return NoContent();
            }
        } 