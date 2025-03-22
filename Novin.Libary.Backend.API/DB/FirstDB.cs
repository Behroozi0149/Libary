using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Novin.Libary.Backend.API.Entities;

namespace Novin.Libary.Backend.API.DB
{
    public class FirstDB : IdentityDbContext<LibraryUser>
    {
        public required DbSet<Book> Books { get; set; }
        public required DbSet<Borrow> Borrows { get; set; }
        public required DbSet<Member> Members { get; set; }
        public FirstDB(DbContextOptions options)
            : base(options)
        {

        }
    }
}