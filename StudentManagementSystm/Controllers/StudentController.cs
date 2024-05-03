using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using StudentManagementSystem.ViewModel;
using System;
using System.Runtime.CompilerServices;

namespace StudentManagementSystm.Controllers
{
    public class StudentController : Controller

    {

        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
              _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Students()
        {
            var students= _context.StudentTable.ToList();
            return View(students);
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
       
 }
        [HttpPost]
        public IActionResult AddStudent(StudentVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var studentvm = new Student()
            {
                Name = vm.Name, 
               Grade = vm.Grade,
               RollNo = vm.RollNo,
               Faculty = vm.Faculty,
               Sex = vm.Sex, 
               Address= vm.Address,

            };
             _context.StudentTable.Add(studentvm);
            _context.SaveChanges();
            return RedirectToAction("Students");
        }

        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            var studentuser = _context.StudentTable.FirstOrDefault (x =>x.Id == id);
            if (studentuser != null)
            {
                var std = new UpdateVM()
                {
                    Name = studentuser.Name,
                    Grade = studentuser.Grade,
                    Address = studentuser.Address,
                    Sex = studentuser.Sex,
                    Faculty = studentuser.Faculty,
                    RollNo = studentuser.RollNo,

                };
                return View(std);
            }
            return RedirectToAction("Students");

        }

        [HttpPost]
        public IActionResult EditStudent(UpdateVM vm)
        {
           if(!ModelState.IsValid)
            {
                return View(vm);
            }
           var student = _context.StudentTable.Find(vm.Id);
           if (student!=null) {
                student.Name = vm.Name;
                student.Grade = vm.Grade;
                student.Address = vm.Address;
                student.Sex = vm.Sex;
                student.Faculty = vm.Faculty;
                student.RollNo = vm.RollNo;

           _context.SaveChanges();
            return RedirectToAction("Students");
           }
            else{
                return View(vm);
            }

           

        }
        public IActionResult Delete(int id)
        {
           var student = _context.StudentTable.Find(id);
            if (student!=null)
            {
                _context.StudentTable.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("Students");
            }
            return View("ERROR");
            
        }

        public IActionResult ERROR()
        {
            return View();
        }

    }
}
