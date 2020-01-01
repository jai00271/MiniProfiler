using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UtilityTool.DataBase.Models
{
    public partial class UtilityToolContext : DbContext
    {
        public UtilityToolContext()
        {
        }

        public UtilityToolContext(DbContextOptions<UtilityToolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TryYourLuck> TryYourLuck { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\Local;Database=UtilityTool;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TryYourLuck>(entity =>
            {
                entity.Property(e => e.Numbers)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
