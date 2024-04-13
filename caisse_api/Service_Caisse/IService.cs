public interface IService<TEntity>{
    Task<List<TEntity>> GetAll();
    Task<TEntity> GetById(int id);
    Task<bool>Add(TEntity entity);
    Task<bool>AddList(List<TEntity> entitys);
    Task<bool>Remove(TEntity entity);
    Task<bool>Update(TEntity entity);
    Task<bool>RemoveById(int id);
    Task save();
}