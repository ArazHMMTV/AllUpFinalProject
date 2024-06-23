using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; }=null!;
        public DbSet<CompanyInfo> CompanyInfos { get; set; } = null!;
        public DbSet<Contact> Contacts { get; set; } = null!;           
        public DbSet<ProductImage> ProductImages { get; set; } = null!;
        public DbSet<Setting> Settings { get; set; } = null!;
        public DbSet<Testimonial> Testimonials { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<Brand> Brands  { get; set; } = null!;
        public DbSet<Blog> Blogs  { get; set; } = null!;
        public DbSet<BlogCategory> BlogCategories  { get; set; } = null!;

    }
}
