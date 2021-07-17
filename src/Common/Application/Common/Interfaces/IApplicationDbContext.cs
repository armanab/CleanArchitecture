using CleanApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CleanApplication.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }

        DbSet<TodoItem> TodoItems { get; set; }
        DbSet<Image> Images { get; set; }

        DbSet<Content> Contents { get; set; }
        DbSet<Setting> Settings { get; set; }
      
        DbSet<Tag> Tags { get; set; }
      
     
      
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        //IQueryable<TEntity> Get<TEntity>(this DbSet<TEntity> _context, Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderByFunc, List<string> includeNavigationProperties = null) where TEntity : class;

    }
}
