using Microsoft.EntityFrameworkCore;
using BusinessLogic;
using Common.Models;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Client> Clients { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<PassportId>();

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Trainer>()
                .HasBaseType<User>();

            // Зв'язок між Subscription і User
            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.User)
                .WithMany(u => u.Subscriptions)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Trainer)
                .WithMany()
                .HasForeignKey(s => s.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Налаштування типу колонки для властивостей
            modelBuilder.Entity<Subscription>()
                .Property(s => s.ExpiryDate)  // Вказати властивість, яку потрібно налаштувати
                .HasColumnType("timestamp with time zone");

            // Налаштування типу колонки для властивості в іншій сутності
            modelBuilder.Entity<Booking>()
                .Property(b => b.BookingDate)
                .HasColumnType("timestamp with time zone");
        }


    }

}