using BussnesLogic.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PPHDbContext : IdentityDbContext
    {
        public PPHDbContext()
        {

        }
        public PPHDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseNpgsql(@"Server=localhost:5433;Database=pph_db;User ID=postgres;Password=Vladadmin2222");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasOne(p => p.Role)
                                                    .WithMany(c => c.Users)
                                                    .HasForeignKey(p => p.RoleId);

            modelBuilder.Entity<PidGanreGame>()
                .HasKey(ba => new { ba.GanrId, ba.GameId });

            modelBuilder.Entity<PidGanreGame>()
                .HasOne(ba => ba.Ganr)
                .WithMany(b => b.Games)
                .HasForeignKey(ba => ba.GanrId);

            modelBuilder.Entity<PidGanreGame>()
                .HasOne(ba => ba.Game)
                .WithMany(a => a.Ganres)
                .HasForeignKey(ba => ba.GameId);

            modelBuilder.SeedGanres();
            modelBuilder.SeedRoles();
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Filters> Filters { get; set; }
        public virtual DbSet<Ganr> Ganrs { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<PidGanreGame> PidGanreGames { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Game> Games { get; set; }

    }
}
