
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class TipoDocumentoRepository : GenericRepository<TipoDocumento>, ITipoDocumento
{
    private readonly FarmaciaContext _context;

    public TipoDocumentoRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }
}