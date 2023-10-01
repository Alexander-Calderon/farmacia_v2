
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;


namespace Application.Repository;
public class CompraProveedorRepository : GenericRepository<CompraProveedor>, ICompraProveedor
{
    private readonly FarmaciaContext _context;

    public CompraProveedorRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }

}