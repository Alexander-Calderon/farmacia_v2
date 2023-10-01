
    using Domain.Entities;
    using Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Persistence;

namespace Application.Repository;
    public class ContactoProveedorRepository: GenericRepository<Cargo>, ICargo
            {
                private readonly FarmaciaContext _context;
            
                public ContactoProveedorRepository(FarmaciaContext context) : base(context)
                {
                    _context = context;
                }
            }