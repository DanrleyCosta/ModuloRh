using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RhModulo.Models;

namespace RhModulo.Data
{
    public class RhModuloContext : DbContext
    {
        public RhModuloContext (DbContextOptions<RhModuloContext> options)
            : base(options)
        {
        }

        public DbSet<RhModulo.Models.FuncionarioPf> FuncionarioPf { get; set; }
    }
}
