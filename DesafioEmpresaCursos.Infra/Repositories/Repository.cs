using DesafioEmpresaCursos.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DesafioEmpresaCursos.Infra.Repositories
{
    public abstract  class Repository<T> where T : class
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<T> DbSet;

        protected Repository(AppDbContext context)
        {
            Db = context;
            DbSet = Db.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task Add(T entity)
        {
            await DbSet.AddAsync(entity);
            await Db.SaveChangesAsync();
        }

        public virtual async Task Update(T entity)
        {
            DbSet.Update(entity);
            await Db.SaveChangesAsync();
        }

        public virtual async Task Delete(Guid id)
        {
            DbSet.Remove(await GetById(id));
            await Db.SaveChangesAsync();
        }
    }
}