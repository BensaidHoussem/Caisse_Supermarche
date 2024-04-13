
using Entities.Data;
using Microsoft.EntityFrameworkCore;

public class Service<TEntity> : IService<TEntity> where TEntity : class
{
    protected readonly IDbContextCaisse _dbContextCaisse;
    protected DbSet<TEntity> _dbSet;
    protected CaisseDbContext _dbcontext=>_dbContextCaisse?.DbContext;

    public Service(IDbContextCaisse dbContextCaisse)
    {
        _dbContextCaisse = dbContextCaisse;
        _dbSet=_dbcontext.Set<TEntity>();
    }
    public async Task<bool> Add(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await save();
        return await Task.FromResult(true);
    }

    public async Task<List<TEntity>> GetAll()
    {
        return Task.FromResult<List<TEntity>>(await _dbSet.ToListAsync()).Result;
    }

    public async Task<TEntity> GetById(int id)
    {
        return Task.FromResult<TEntity>(await _dbSet.FindAsync(id)).Result;

    }

    public async Task<bool> Remove(TEntity entity)
    {
        bool k=_dbSet.Contains(entity);
        if (k){
            _dbSet.Remove(entity);
            await save();
            return await Task.FromResult(true);
        }
        return await Task.FromResult(false);
        
    }

    public async Task<bool> RemoveById(int id)
    {
        TEntity entity = await GetById(id);
        if (entity != null){
            _dbSet.Remove(entity);
            await save();
            return await Task.FromResult(true);            
        }
        return await Task.FromResult(false);

    }



    public async Task<bool> Update(TEntity entity)
    {
        _dbSet.Update(entity);
        _dbcontext.ConfigureAwait(false);
        await save();
        return await Task.FromResult(true);

    }

    public async Task save()
    {
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<bool> AddList(List<TEntity> entitys)
    {
         await _dbSet.AddRangeAsync(entitys).ConfigureAwait(false);
         await save();
         return Task.FromResult(true).Result;
    }
}