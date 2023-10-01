
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class DetalleFacturaRepository : GenericRepository<DetalleFactura>, IDetalleFactura
{
    private readonly FarmaciaContext _context;

    public DetalleFacturaRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }
}