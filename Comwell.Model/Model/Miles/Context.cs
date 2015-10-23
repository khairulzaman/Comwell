namespace Comwell.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Context : DbContext
    {
        public Context()
            : base("name=LocalLGContext")
        {
        }

        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Revision> Revisions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.Number)
                .IsUnicode(false);

            modelBuilder.Entity<Document>()
                .Property(e => e.Timestamp)
                .IsFixedLength();

            modelBuilder.Entity<Revision>()
                .Property(e => e.Timestamp)
                .IsFixedLength();
        }
    }
}
