using Microsoft.EntityFrameworkCore;

namespace projectcar.Models
{
    public class CarContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ShoppingCar;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasIndex(u => u.Email).IsUnique(unique: true);
            modelBuilder.Entity<UserModel>().HasIndex(u => u.Username).IsUnique(unique:true);
            modelBuilder.Entity<UserModel>().HasIndex(u => u.Phone).IsUnique(unique: true);
            modelBuilder.Entity<CarModel>().HasKey(c => c.VIN);
            modelBuilder.Entity<CarModel>().Property(c=>c.VIN).ValueGeneratedNever();
            modelBuilder.Entity<BodyStyleModel>().HasIndex(bs => bs.Name).IsUnique();
            modelBuilder.Entity<ModelModel>().HasIndex(bs => new {bs.Name,bs.BodyStyleId,bs.MakerId,bs.Yearmodel}).IsUnique();
            modelBuilder.Entity<MakerModel>().HasIndex(bs => bs.Name).IsUnique();


            //relationships
            //one to many
            //car
            modelBuilder.Entity<CarModel>().HasOne<UserModel>(c => c.User).WithMany(u => u.Cars).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CarModel>().HasOne<ModelModel>(c=>c.Model).WithMany(m => m.Cars).HasForeignKey(c => c.ModelId).OnDelete(DeleteBehavior.Cascade);
            //model
            modelBuilder.Entity<ModelModel>().HasOne<MakerModel>(m=>m.Maker).WithMany(ma=>ma.Models).HasForeignKey(m=>m.MakerId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ModelModel>().HasOne<BodyStyleModel>(m => m.BodyStyle).WithMany(b => b.Models).HasForeignKey(m => m.BodyStyleId).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ModelModel>  Models { get; set; }
        public DbSet<CarModel> Cars { get; set; }
        public DbSet<BodyStyleModel> BodyStyles { get; set; }
        public DbSet<MakerModel> Makers { get; set; }
        public DbSet<ContactModel> contacts { get; set; }

    }
}
