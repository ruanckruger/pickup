namespace Pickup
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PickupModel : DbContext
    {
        public PickupModel()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                .HasMany(e => e.Players)
                .WithOptional(e => e.Match)
                .HasForeignKey(e => e.CurMatch);

            modelBuilder.Entity<Player>()
                .Property(e => e.PreferedRole)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.CurRole)
                .IsUnicode(false);
        }
    }
}
