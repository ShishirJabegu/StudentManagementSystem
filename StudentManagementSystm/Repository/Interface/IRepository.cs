using System.Linq.Expressions;

namespace StudentManagementSystem.Repository.Interface
{
    public interface IRepository <T> where T : class
    {
        void Add(T entity);
        void Remove (T entity);
        List<T> GetAll ();
        T Get (Expression<Func<T, bool>> predicate);
        public T FindById(int id);

    }
}
