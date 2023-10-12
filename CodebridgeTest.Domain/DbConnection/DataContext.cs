using CodebridgeTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodebridgeTest.Domain.DbConnection;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> opts) : base(opts) { }


    public DbSet<Dog> Dogs { get; set; }
}
