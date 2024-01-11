using Crud.Data;

namespace Crud.Repositories
{
    public interface IGenericRepos
    {
        Task<List<T>> GetAll<T>() where T:class;
        Task<T> GetOne<T>(int id) where T:class;
        Task<T> Add<T>(T tObj) where T : class;

        Task <T>Update<T>(int id, T tObj) where T : class;

        Task Delete<T>(int id) where T : class;
    }



}
