using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SuperShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Helper
{
    public class SuperShopDBContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public SuperShopDBContext()
        {

        }
        public SuperShopDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SuperShopDBContext(DbContextOptions<SuperShopDBContext> options)
            : base(options) { }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<SubCategory> SubCategory { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Outlet> Outlet { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<QtyType> QtyType { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        //public virtual DbSet<StockHistory> StockHistory { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<ProductImage> ProductImage { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ProductImage>().HasNoKey();
            //modelBuilder.Entity<UserWiseProjectPermission>().HasNoKey();
            //modelBuilder.Entity<UserWiseProjectRolePermission>().HasNoKey();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
