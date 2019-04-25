using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VOAprototype.Models;

namespace VOAprototype.Models
{
    public class VOAprototypeContext : DbContext
    {
        public VOAprototypeContext (DbContextOptions<VOAprototypeContext> options)
            : base(options)
        {
        }

        public DbSet<VOAprototype.Models.PurchaseOrder> PurchaseOrder { get; set; }

        public DbSet<VOAprototype.Models.ITFunction> ITFunction { get; set; }
    }
}
