using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Novin.Libary.Backend.API.Entities
{
    public class LibraryUser:IdentityUser
    {
        public string? fullname { get; set; }
    }
}