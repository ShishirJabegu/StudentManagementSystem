using AspNetCoreHero.ToastNotification.Abstractions;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repository.Interface;
using System.Linq.Expressions;

namespace StudentManagementSystem.Repository.Implementation
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public ApplicationDbContext _studentDb;
        public INotyfService _notification { get; }
        public StudentRepository(ApplicationDbContext studentDb, INotyfService  notification) : base(studentDb)
        { 
            _studentDb = studentDb;
            _notification = notification;
        }

        public void Save()
        {
            _studentDb.SaveChanges();
        }

        public void Update(Student student)
        {
            _studentDb.Update(student);
        }   
        public void Notify(string message)
        {
            _notification.Success(message);
        }
        
    }
}
