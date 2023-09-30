using Domain.Entities;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Persistence;
    public class FarmaciaContext : DbContext
        {
            public FarmaciaContext(DbContextOptions<FarmaciaContext> options) : base(options)
            {
            
            }

            public DbSet<Cargo> Cargos  { get; set; }
            public DbSet<Categoria> Categorias  { get; set; }
            public DbSet<Ciudad> Ciudades  { get; set; }
            public DbSet<CompraProveedor> CompraProveedores  { get; set; }
            public DbSet<Contacto> Contactos  { get; set; }
            public DbSet<ContactoDetalle> ContactoDetalles  { get; set; }
            public DbSet<Departamento> Departamentos  { get; set; }
            public DbSet<DetalleFactura> DetalleFacturas  { get; set; }
            public DbSet<Direccion> Direcciones  { get; set; }
            public DbSet<Doctor> Doctores  { get; set; }
            public DbSet<Empleado> Empleados  { get; set; }
            public DbSet<Especialidad> Especialidades  { get; set; }
            public DbSet<Estado> Estados  { get; set; }
            public DbSet<Factura> Facturas  { get; set; }
            public DbSet<Marca> Marcas  { get; set; }
            public DbSet<Medicamento> Medicamentos  { get; set; }
            public DbSet<Paciente> Pacientes  { get; set; }
            public DbSet<Pais> Paises  { get; set; }
            public DbSet<Proveedor> Proveedores  { get; set; }
            public DbSet<Receta> Recetas  { get; set; }
            public DbSet<Rol> Roles  { get; set; }
            public DbSet<TipoContacto> TipoContactos  { get; set; }
            public DbSet<TipoDocumento> TipoDocumentos  { get; set; }
            public DbSet<TipoPresentacion> TipoPresentaciones  { get; set; }
            public DbSet<User> Users  { get; set; }
            
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            }
        }
