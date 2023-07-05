using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kitaabKhana.Models;
using Microsoft.EntityFrameworkCore;

namespace kitaabKhana.Data
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
 
        // this 👇 creates a table in table
        // with name "Categories"
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category {
                    CategoryId=1,Name="Action",DisplayOrder=5
                },
                new Category {
                    CategoryId=2,Name="Sci-Fi",DisplayOrder=9
                },
                new Category {
                    CategoryId=3,Name="RomCom",DisplayOrder=2
                },
                new Category {
                    CategoryId=4,Name="Drama",DisplayOrder=7
                }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}