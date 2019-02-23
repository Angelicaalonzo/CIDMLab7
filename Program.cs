using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CIDMLab7
{
    class Program
    {

        static void List()
        {
            using (var db = new AppDbContext())
            {
                var total =db.Courses.Include(m => m.StudentCourses).ThenInclude(mn => mn.Student);

                foreach(var course in total)
                {
                    Console.WriteLine($"{course.CourseName}-  ");
                    foreach (var student in course.StudentCourses)
                    {

                        Console.WriteLine($" \t {student.Student.FirstName} {student.Student.LastName}  {student.GPA}");

                    }
                    Console.WriteLine(); 
                }

            }
        }

        static void removeOne(){
            using (var db = new AppDbContext())
            {
                Student studentToRemove = db.Students.Where(m => m.FirstName == "Angelica").First(); 

            }
        }
        static void Main(string[] args)
        {
             using(var db = new AppDbContext()){
                db.Database.EnsureDeleted(); 
                db.Database.EnsureCreated(); 

                 List<Student> students = new List<Student>(){
                    new Student {FirstName = "Angelica", LastName= "Alonzo" },
                    new Student {FirstName = "Ryan", LastName = "Gross"},
                    new Student {FirstName= "Walter", LastName= "Lasley"},
                    new Student {FirstName ="Greg", LastName= "sprock"}
                };

                List<Course> courses = new List<Course>(){
                    new Course {CourseName = "Advanced Business Programming"},
                    new Course {CourseName = "Mobile App Development"}
                };

               
                List<StudentCourse> joinTable = new List<StudentCourse>(){
                    new StudentCourse{ Student = students[0], Course = courses[0], GPA = 3.5m},
                    new StudentCourse{ Student = students[1], Course = courses[0],  GPA = 3.0m},
                    new StudentCourse{ Student = students[2], Course = courses[1],GPA = 2.2m},
                    new StudentCourse{ Student = students[3], Course = courses[1],  GPA = 4.8m}

                };
                db.AddRange(courses);
                db.AddRange(students);
                db.AddRange(joinTable);
                db.SaveChanges(); 
             }

             //lists out all courses and students with their GPA
             List(); 
             removeOne(); 
         }




    }
}
