using CodeFirstUnitOfWork.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeFirstUnitOfWork.DBContext
{
    public class PoCContext : DbContext
    {
        public PoCContext(DbContextOptions<PoCContext> options)
        : base(options)
        {
        }
        public virtual DbSet<PoCXp> POC_PARTNER_XP { get; set; }

    }
}
