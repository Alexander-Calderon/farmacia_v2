using Application.Repository;
using Domain.Interfaces;
using Persistence;

namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private readonly FarmaciaContext _context;

        private CargoRepository _cargos;
        private CategoriaRepository _categorias;
        private CiudadRepository _cuidades;
        private CompraProveedorRepository _compraProveedores;
        private ContactoRepository _contactos;
        private DepartamentoRepository _departamentos;
        private DetalleFacturaRepository _detalleFacturas;
        private DireccionRepository _direcciones;
        private DoctorRepository _doctores;
        private EmpleadoRepository _empleados;
        private EspecializacionRepository _especialidades;
        private EstadoRepository _estados;
        private FacturaRepository _facturas;
        private MarcaRepository _marcas;
        private MedicamentoRepository _medicamentos;
        private PacienteRepository _pacientes;
        private PaisRepository _paises;
        private ProveedorRepository _proveedores;
        private RecetaRepository _recetas;
        private RolRepository _roles;
        private TipoContactoRepository _tipoContactos;
        private TipoDocumentoRepository _tipoDocumentos;
        private TipoPresentacionRepository _tipoPresentaciones;

        public UnitOfWork(FarmaciaContext context)
        {
            _context = context;
        }

        // Controll de nulos para los repositorios
        public ICargo Cargos
        {
            get
            {
                if (_cargos == null)
                {
                    _cargos = new CargoRepository(_context);
                }
                return _cargos;
            }
        }

        public ICategoria Categorias
        {
            get
            {
                if (_categorias == null)
                {
                    _categorias = new CategoriaRepository(_context);
                }
                return _categorias;
            }
        }

        public ICiudad Cuidades
        {
            get
            {
                if (_cuidades == null)
                {
                    _cuidades = new CiudadRepository(_context);
                }
                return _cuidades;
            }
        }

        public ICompraProveedor CompraProveedores
        {
            get
            {
                if (_compraProveedores == null)
                {
                    _compraProveedores = new CompraProveedorRepository(_context);
                }
                return _compraProveedores;
            }
        }

        public IContacto Contactos
        {
            get
            {
                if (_contactos == null)
                {
                    _contactos = new ContactoRepository(_context);
                }
                return _contactos;
            }
        }

        public IDepartamento Departamentos
        {
            get
            {
                if (_departamentos == null)
                {
                    _departamentos = new DepartamentoRepository(_context);
                }
                return _departamentos;
            }
        }

        public IDetalleFactura DetalleFacturas
        {
            get
            {
                if (_detalleFacturas == null)
                {
                    _detalleFacturas = new DetalleFacturaRepository(_context);
                }
                return _detalleFacturas;
            }
        }

        public IDireccion Direcciones
        {
            get
            {
                if (_direcciones == null)
                {
                    _direcciones = new DireccionRepository(_context);
                }
                return _direcciones;
            }
        }

        public IDoctor Doctores
        {
            get
            {
                if (_doctores == null)
                {
                    _doctores = new DoctorRepository(_context);
                }
                return _doctores;
            }
        }

        public IEmpleado Empleados
        {
            get
            {
                if (_empleados == null)
                {
                    _empleados = new EmpleadoRepository(_context);
                }
                return _empleados;
            }
        }

        public IEspecializacion Especializacion
        {
            get
            {
                if (_especialidades == null)
                {
                    _especialidades = new EspecializacionRepository(_context);
                }
                return _especialidades;
            }
        }

        public IEstado Estados 
        {
            get
            {
                if (_estados == null)
                {
                    _estados = new EstadoRepository(_context);
                }
                return _estados;
            }
        }

        public IFactura Facturas 
        {
            get
            {
                if (_facturas == null)
                {
                    _facturas = new FacturaRepository(_context);
                }
                return _facturas;
            }
        }

        public IMarca Marcas 
        {
            get
            {
                if (_marcas == null)
                {
                    _marcas = new MarcaRepository(_context);
                }
                return _marcas;
            }
        }

        public IMedicamento Medicamentos
        {
            get
            {
                if (_medicamentos == null)
                {
                    _medicamentos = new MedicamentoRepository(_context);
                }
                return _medicamentos;
            }
        }

        public IPaciente Pacientes
        {
            get
            {
                if (_pacientes == null)
                {
                    _pacientes = new PacienteRepository(_context);
                }
                return _pacientes;
            }
        }

        public IPais Paises
        {
            get
            {
                if (_paises == null)
                {
                    _paises = new PaisRepository(_context);
                }
                return _paises;
            }
        }

        public IProveedor Proveedores
        {
            get
            {
                if (_proveedores == null)
                {
                    _proveedores = new ProveedorRepository(_context);
                }
                return _proveedores;
            }
        }

        public IReceta Recetas
        {
            get
            {
                if (_recetas == null)
                {
                    _recetas = new RecetaRepository(_context);
                }
                return _recetas;
            }
        }

        public IRol Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new RolRepository(_context);
                }
                return _roles;
            }
        }

        public ITipoContacto TipoContactos
        {
            get
            {
                if (_tipoContactos == null)
                {
                    _tipoContactos = new TipoContactoRepository(_context);
                }
                return _tipoContactos;
            }
        }

        public ITipoDocumento TipoDocumentos
        {
            get
            {
                if (_tipoDocumentos == null)
                {
                    _tipoDocumentos = new TipoDocumentoRepository(_context);
                }
                return _tipoDocumentos;
            }
        }

        public  ITipoPresentacion TipoPresentaciones
        {
            get
            {
                if (_tipoPresentaciones == null)
                {
                    _tipoPresentaciones = new TipoPresentacionRepository(_context);
                }
                return _tipoPresentaciones;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }





    }
}