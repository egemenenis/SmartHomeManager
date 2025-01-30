using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Models;

namespace SmartHomeManager.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SmartHomeManager.Models;
    using System.Collections.Generic;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Thermostat> Thermostats { get; set; }
        public DbSet<Camera> Cameras { get; set; }
        public DbSet<DoorLock> DoorLocks { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<AccessLevel> AccessLevels { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Device>()
                .HasDiscriminator<string>("DeviceType")
                .HasValue<Device>("Device")
                .HasValue<Camera>("Camera")
                .HasValue<Thermostat>("Thermostat")
                .HasValue<DoorLock>("DoorLock")
                .HasValue<Sensor>("Sensor");
        }
    }
}