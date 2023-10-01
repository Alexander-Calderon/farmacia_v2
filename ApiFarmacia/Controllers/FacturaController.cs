using ApiFarmacia.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIFarmacia.Controllers;

    public class FacturaController : ApiBaseController
    {
        private readonly IUnitOfWork unitofwork;
        private readonly IMapper mapper;
    
        public FacturaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitofwork = unitOfWork;
            this.mapper = mapper;
        }
    
    
        [HttpGet]
        // [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<FacturaDto>>> Get()
        {
            var factura = await unitofwork.Facturas.GetAllAsync();
            return mapper.Map<List<FacturaDto>>(factura);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<FacturaDto>> Get(int id)
        {
            var factura = await unitofwork.Facturas.GetByIdAsync(id);
            if (factura == null){
                return NotFound();
            }
            return this.mapper.Map<FacturaDto>(factura);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Factura>> Post(FacturaDto facturaDto)
        {
            var factura = this.mapper.Map<Factura>(facturaDto);
            this.unitofwork.Facturas.Add(factura);
            await unitofwork.SaveAsync();
            if(factura == null)
            {
                return BadRequest();
            }
            facturaDto.Id = factura.Id;
            return CreatedAtAction(nameof(Post), new {id = facturaDto.Id}, facturaDto);
        }
        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<FacturaDto>> Put(int id, [FromBody]FacturaDto facturaDto){
            if(facturaDto == null)
            {
                return NotFound();
            }
            var factura = this.mapper.Map<Factura>(facturaDto);
            unitofwork.Facturas.Update(factura);
            await unitofwork.SaveAsync();
            return facturaDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Delete(int id){
            var factura = await unitofwork.Facturas.GetByIdAsync(id);
            if(factura == null)
            {
                return NotFound();
            }
            unitofwork.Facturas.Remove(factura);
            await unitofwork.SaveAsync();
            return NoContent();
        }
    } 