using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace ApiFarmacia.Dtos;

    public class UserDto:BaseEntity
    {
        public string UserName {get; set;}
        public string Password {get; set;}

        public int IdRolFk {get; set;}
    }