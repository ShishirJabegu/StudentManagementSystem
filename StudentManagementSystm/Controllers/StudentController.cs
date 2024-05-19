using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repository.Interface;
using StudentManagementSystem.ViewModel;
using System;
using System.Runtime.CompilerServices;

namespace StudentManagementSystm.Controllers
{
    public class StudentController : Controller

    {
        private readonly IStudentRepository _studentRepo;

        public StudentController(IStudentRepository studentRepo)
        {
            
            _studentRepo= studentRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Students()
        {
            var students= _studentRepo.GetAll();
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
                _studentRepo.Notify("Failed to Registered!!");
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
            _studentRepo.Add(studentvm);
            _studentRepo.Save();
            _studentRepo.Notify("Successfully Created");
            return RedirectToAction("Students");
        }
        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            var studentuser = _studentRepo.Get (x =>x.Id == id);
            if (studentuser != null)
            {
                var std = new UpdateVM
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
                _studentRepo.Notify("Failed to Update");
                return View(vm);
            }
           var student = _studentRepo.FindById(vm.Id);
           if (student!=null) {
                student.Name = vm.Name;
                student.Grade = vm.Grade;
                student.Address = vm.Address;
                student.Sex = vm.Sex;
                student.Faculty = vm.Faculty;
                student.RollNo = vm.RollNo;
                _studentRepo.Save();
                _studentRepo.Notify("Successfully Updated");
                return RedirectToAction("Students");
           }
            else{
                return View(vm);
            }        
        }
              
        public IActionResult Delete(int id)
        {
           var student = _studentRepo.FindById(id);
            if (student!=null)
            {
                _studentRepo.Remove(student);
                _studentRepo.Save();
                _studentRepo.Notify("Successfully Deleted");
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
