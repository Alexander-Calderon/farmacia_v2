using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;


namespace Application.Repository;
public class DireccionRepository : GenericRepository<Direccion>, IDireccion
{
    private readonly FarmaciaContext _context;

    public DireccionRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }
}