
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class DoctorRepository : GenericRepository<Doctor>, IDoctor
{
    private readonly FarmaciaContext _context;

    public DoctorRepository(FarmaciaContext context) : base(context)
    {
        _context = context;
    }
}