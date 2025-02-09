using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Novin.Libary.Backend.API.Entities.Base;

namespace Novin.Libary.Backend.API.Entities
{
    // عضو
    public class Member : Thing
    {
        public string? Fullname { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
}