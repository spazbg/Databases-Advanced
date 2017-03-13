namespace BookShopSystem.Data
{
    using Migrations;
    using Models;
    using System;
    using System.Data.Entity;

    public class BookShopContext : DbContext
    {
        public BookShopContext()
            : base("name=BookShopContext")
        {
            //Database.SetInitializer<BookShopContext>(new MigrateDatabaseToLatestVersion<BookShopContext, Configuration>()); // Step 4 => in Main
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) // Step 6 Related Books Mapping table
        {
            modelBuilder.Entity<Book>()
                .HasMany(b => b.RelatedBooks)
                .WithMany() // not specified => allow data loss for this update
                .Map(m =>
                {
                    m.ToTable("RelatedBooks");
                    m.MapLeftKey("BookId");
                    m.MapRightKey("RelatedBookId");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}