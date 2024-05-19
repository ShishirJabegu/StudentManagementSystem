using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Repository.Interface;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace StudentManagementSystem.Repository.Implementation
{

    public class Repository <T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _studentDb;
        public DbSet<T> database;
        public Repository(ApplicationDbContext studentDb ) 
        { 
            _studentDb = studentDb;
            this.database = studentDb.Set<T>();
        }

        public void Add(T entity)
        {
            database.Add(entity);
        }

        public T FindById(int id)
        {
            return database.Find(id);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = database;
            query = query.Where(filter);
            return query.FirstOrDefault(); 
        }

        public List<T> GetAll()
        {
            return database.ToList();
        }

        public void Remove(T entity)
        {
            database.Remove(entity);
        }
    }
}
