using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CIDMLab7{


    public class AppDbContext: DbContext 
    {

 protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(e => new {e.StudentID, e.CourseID});
        }
        public DbSet<Student> Students {get; set; }
        public DbSet <Course> Courses {get; set;}
        public DbSet <StudentCourse> StudentCourses{get; set; }
    }

    

    public class Course {
        public int CourseID{get; set; }
        public string CourseName{get; set;}
        public List<StudentCourse> StudentCourses{get; set;}

    }
    public class Student{
        public int StudentID {get; set; }
        public string FirstName{get; set; }
        public string LastName{get; set;}
        public List <StudentCourse> StudentCourses {get; set;}

    }
    
    public class StudentCourse{
        public int StudentID {get; set; }//composit PK, FK 1
        public int CourseID {get; set;} //Composit Pk, FK 2
        public Student Student {get; set; }//Navigation Property 
        public Course Course{get; set;} //Navigation Property
        public decimal GPA {get; set;}
    }

}
