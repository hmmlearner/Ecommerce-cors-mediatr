using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public interface IApplicationDbcontext
    {
        public DbSet<Category> Categories { get;  }
        public DbSet<Product> Products { get;  }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}