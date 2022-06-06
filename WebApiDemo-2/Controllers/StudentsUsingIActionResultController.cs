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
    public class StudentsUsingIActionResultController : ControllerBase
    {
        static List<Student> listStudents = null;
        public StudentsUsingIActionResultController()
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
        public IActionResult Get()
        {
            if (listStudents.Count == 0)
                return NotFound();
            else
                return Ok(listStudents.ToList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetStudentById(int id)
        {
            var obj = listStudents.FirstOrDefault(x => x.StudentId == id);
            if (obj == null)
                return NotFound();
            else
                return Ok(obj);
        }
        [HttpPost]
        public IActionResult Post(Student student)
        {
            listStudents.Add(student);
            return Created("Record Inserted", student);
        }

        [HttpPut]
        [Route("{id:int}")]

        public IActionResult Edit(int id, Student student)
        {
            var obj = GetStudentById(id);
            if (obj != null)
            {
                foreach (var temp in listStudents)
                {
                    if (temp.StudentId == id)
                    {
                        temp.Marks = student.Marks;
                        temp.DateOfBirth = student.DateOfBirth;

                    }
                }
                return Ok("Edited");
            }
            else
                return NotFound();

        }
    
        [HttpDelete]
        [Route("{id:int}")]

        public IActionResult Delete(int id)
        {
            var obj = GetStudentById(id);
            if (obj != null)
            {
                var temp = listStudents.Find(x => x.StudentId == id);
                listStudents.Remove(temp);
                return Ok("Deleted");
            }
            else
                return NotFound();
             
        }



    }
}
