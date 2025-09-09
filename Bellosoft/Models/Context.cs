using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Bellosoft.Models
{ 
        public class Context : DbContext
        {
            public Context(DbContextOptions<Context> options) : base(options) { }

            public DbSet<NasaApod> NasaApods { get; set; }
        } 
}
