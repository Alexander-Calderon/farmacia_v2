using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;


namespace Application.Repository;
public class ContactoEmpleadoRepository : GenericRepository<Cargo>, ICargo
{
    private readonly FarmaciaContext _context;

    public ContactoEmpleadoRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }
}
