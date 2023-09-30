using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;

    public class User:BaseEntity
    {
        public string UserName {get; set;}
        public string Password {get; set;}

        public int IdRolFk {get; set;}
        public Rol Rol {get; set;}
        public ICollection<Empleado> Empleados {get; set;}
    }
