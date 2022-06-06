using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo_2.Models;

namespace WebApiDemo_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
       static List<Student> listStudents = null;
        public StudentsController()
        {
            if (listStudents == null)
                InitializeStudents();
        }
        void InitializeStudents()
        {
            if (listStudents == null)
            {
                listStudents = new List<Student>()
                 {

               new Student() { StudentId=1, Name="Ajay" , Batch="B001", Marks=89, DateOfBirth=Convert.ToDateTime("12/12/2020")},

               new Student() { StudentId=2, Name="Deepak" , Batch="B002", Marks=78, DateOfBirth=Convert.ToDateTime("10/06/2020")},
           };

            }

        }
        [HttpGet]
        public List<Student> Get()
        {
            return listStudents.ToList();
        }

        [HttpGet]
        [Route("{id:int}")]
        public  Student GetStudentById(int id)
        {
            return listStudents.FirstOrDefault(x => x.StudentId == id);
        }
        [HttpPost]
        public void Post(Student student)
        {
            listStudents.Add(student);
        }

        [HttpPut]
        [Route("{id:int}")]

        public Student Edit(int id,  Student student)
        {
            var obj = GetStudentById(id);
            if(obj!=null)
            {
                foreach(var temp in listStudents)
                {
                    if(temp.StudentId == id)
                    {
                        temp.Marks = student.Marks;
                        temp.DateOfBirth = student.DateOfBirth;

                    }
                }
            }
            return student;
        }

        [HttpDelete]
        [Route("{id:int}")]

        public void Delete(int id)
        {
            var obj = GetStudentById(id);
            if (obj != null)
            {
                listStudents.Remove(obj);
            }
             
        }



    }
}
