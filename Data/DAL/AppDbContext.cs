using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Category> Categories {  get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CompanyInfo> CompanyInfos { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Setting> Settings  { get; set; }
        public DbSet<Tag> Tags  { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Core.Models.Type> Types  { get; set; }

    }
}
