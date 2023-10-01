using Domain.Entities;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Persistence;
    public class FarmaciaContext : DbContext
        {
            public FarmaciaContext(DbContextOptions<FarmaciaContext> options) : base(options)
            {
            
            }

            // public DbSet<Cargo> CARGOS { get; set; }
            // public DbSet<Categoria> CATEGORIAS  { get; set; }
            // public DbSet<Ciudad> CIUDADES  { get; set; }
            // public DbSet<CompraProveedor> COMPRAS_PROVEEDORES  { get; set; }
            // public DbSet<Contacto> CONTACTOS  { get; set; }
            // public DbSet<Contacto> CONTACTOS_EMPLEADOS { get; set; }            
            // public DbSet<Contacto> CONTACTOS_PROVEEDORES { get; set; }            
            // public DbSet<Departamento> DEPARTAMENTOS  { get; set; }
            // public DbSet<DetalleFactura> DETALLES_FACTURAS  { get; set; }
            // public DbSet<Direccion> DIRECCIONES  { get; set; }
            // public DbSet<Doctor> DOCTORES  { get; set; }
            // public DbSet<Empleado> EMPLEADOS  { get; set; }
            // public DbSet<Especializacion> ESPECIALIZACIONES_DOCTORES  { get; set; }
            // public DbSet<Estado> ESTADOS_FACTURA  { get; set; }
            // public DbSet<Factura> FACTURAS  { get; set; }
            // public DbSet<Marca> MARCAS  { get; set; }
            // public DbSet<Medicamento> MEDICAMENTOS  { get; set; }
            // public DbSet<Paciente> PACIENTES  { get; set; }
            // public DbSet<Pais> PAISES  { get; set; }
            // public DbSet<Proveedor> PROVEEDORES  { get; set; }
            // public DbSet<Receta> RECETAS  { get; set; }
            // public DbSet<Rol> ROLES  { get; set; }
            // public DbSet<TipoContacto> TIPOS_CONTACTOS  { get; set; }
            // public DbSet<TipoDocumento> TIPOS_DOCUMENTOS  { get; set; }
            // public DbSet<TipoPresentacion> TIPOS_PRESENTACIONES  { get; set; }
            // public DbSet<User> USERS  { get; set; }
            
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
