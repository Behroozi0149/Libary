using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Novin.Libary.Backend.API.Entities.Base;

namespace Novin.Libary.Backend.API.Entities
{
    public class Book : Thing
    {
        // کتاب
        public string? Title { get; set; }
        public string? Writer { get; set; }
        public double Price { get; set; }
    }
}