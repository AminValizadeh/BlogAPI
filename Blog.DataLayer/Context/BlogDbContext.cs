using Blog.DataLayer.Entities.Access;
using Blog.DataLayer.Entities.Account;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataLayer.Context
{
    public class BlogDbContext : DbContext
    {
        #region constructor

        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        #endregion


        #region Db Sets

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }


        #endregion

        #region disable cascading delete in database

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascades = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascades)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        #endregion

    }
}
