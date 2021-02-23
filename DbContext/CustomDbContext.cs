
using SampleAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.DbContext
{
    public class CustomDbContext : IdentityDbContext
    {
        public CustomDbContext()
        {
        }

        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options)
        {

        }

        
        public DbSet<MemberShip> Members { get; set; }
        public DbSet<User> User { get; set; }
        //public DbSet<MemberSponserRelationTable> MemberSponserRelations {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



        }

    }
}
