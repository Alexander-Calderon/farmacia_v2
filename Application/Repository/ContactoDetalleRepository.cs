
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class ContactoDetalleRepository: GenericRepository<ContactoDetalle>, IContactoDetalle
{
    private readonly FarmaciaContext _context;

    public ContactoDetalleRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }
}