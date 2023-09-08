using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CrudApp.Models;
using Crud.Models;

namespace Crud.Data
{
    public class CrudContext : DbContext
    {
        public CrudContext (DbContextOptions<CrudContext> options)
            : base(options)
        {
        }

        public DbSet<CrudApp.Models.Information> Information { get; set; } = default!;

    

        

        public DbSet<CrudApp.Models.Contact>? Contact { get; set; }

        public DbSet<CrudApp.Models.Education>? Education { get; set; }

        public DbSet<CrudApp.Models.Experience>? Experience { get; set; }

        public DbSet<CrudApp.Models.Skills>? Skills { get; set; }

        

        

       

     

      

   
    }
}
