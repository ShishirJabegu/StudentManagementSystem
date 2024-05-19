using Microsoft.EntityFrameworkCore.Diagnostics;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repository.Interface
{
    public interface IStudentRepository : IRepository<Student>
    {
        void Save();
        void Update(Student student);
        void Notify(string message);
        
    }
}
