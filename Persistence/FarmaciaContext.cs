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
            public DbSet<ContactoEmpleado> ContactoEmpleados { get; set; }
            public DbSet<ContactoProveedor> ContactoProveedores { get; set; }
            public DbSet<Departamento> Departamentos  { get; set; }
            public DbSet<DetalleFactura> DetalleFacturas  { get; set; }
            public DbSet<Direccion> Direcciones  { get; set; }
            public DbSet<Doctor> Doctores  { get; set; }
            public DbSet<Empleado> Empleados  { get; set; }
            public DbSet<Especializacion> Especializaciones  { get; set; }
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

                // Con la siguiete línea, EF Core detecta las configuraciones en el ensamblado referenciado y las aplica automáticamente al construir el modelo,
                // por lo que los nombres de las tablas en los dbsets son ignorados y se usan los asignados desde las configuraciones,
                // en caso de no existir, sacará el nombre de la tabla en base al nombre de la entidad, si se comenta la línea, tomará los nombres de los dbset.                
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
                // y en ese caso se usaría la otra forma para asignar renombrado a los campos en la bd directamente en las entidades, ej: 
                // [Column("id_producto")]
                // public int Id {get; set;}
            }
        }
