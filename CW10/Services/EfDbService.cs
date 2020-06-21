using cw10.DTOs;
using cw10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw10.Services
{
    public class EfDbService : IDbService
    {
        public s16534Context _context;

        public EfDbService(s16534Context context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _context.Student.ToList();
        }


        public Student ModifyStudent(Student student)
        {
            var modifiedStudent = _context.Student.Single(s => s.IndexNumber.Equals(student.IndexNumber));

            if (modifiedStudent != null)
            {
                modifiedStudent.IndexNumber = student.IndexNumber;
                modifiedStudent.FirstName = student.FirstName;
                modifiedStudent.LastName = student.LastName;
                modifiedStudent.BirthDate = student.BirthDate;
                modifiedStudent.IdEnrollment = student.IdEnrollment;
                _context.SaveChanges();
                return modifiedStudent;
            }
            return null;
        }

        public Student DeleteStudent(String IndexNumber)
        {
            //Student o konkretnym indeksie
            var studentToDelete = _context.Student.Single(s => s.IndexNumber.Equals(IndexNumber));
            _context.Student.Remove(studentToDelete);
            _context.SaveChanges();
            return studentToDelete;
        }

        public Student CreateStudent(Student _student)
        {
            _context.Student.Add(_student);
            _context.SaveChanges();
            return _context.Student.Single(s => s.IndexNumber.Equals(_student.IndexNumber));

        }

        public IEnumerable<PromoteStudentResponse> PromoteStudent(PromoteStudentRequest promoteStudentRequest)
        {
            var responsesList = new List<PromoteStudentResponse>();

            //SELECTY
            //Kierunek o podanym skrócie
            var study = _context.Studies.Single(s => s.Name == promoteStudentRequest.Name);

            //Wpis dla podanego kierunku na konkretny semstr
            var enrollment = _context.Enrollment.Single(e => e.Semester == promoteStudentRequest.Semester && e.IdStudy == study.IdStudy);

            //Wpis dla podanego kierunku na następny semestr
            var newEnrollment = _context.Enrollment.Single(e => e.Semester == promoteStudentRequest.Semester + 1 && e.IdStudy == study.IdStudy);

            //Ostatnie ID + 1
            var newIdEnrollment = _context.Enrollment.Max(e => e.IdEnrollment) + 1;


            if (newEnrollment == null)
            {
                _context.Enrollment.Add(new Enrollment
                {
                    IdEnrollment = newIdEnrollment,
                    Semester = promoteStudentRequest.Semester + 1,
                    IdStudy = study.IdStudy,
                    StartDate = DateTime.Now


                });
                newEnrollment = _context.Enrollment.Single(e => e.Semester == promoteStudentRequest.Semester + 1 && e.IdStudy == study.IdStudy);
            }
            var students = _context.Student.Where(student => student.IdEnrollment == enrollment.IdEnrollment).ToList();

            foreach (var student in students)
            {
                student.IdEnrollment = newEnrollment.IdEnrollment;
            }

            var promoteStudentResponse = new PromoteStudentResponse
            {
                IdEnrollment = newEnrollment.IdEnrollment,
                Semester = promoteStudentRequest.Semester + 1,
                IdStudy = study.IdStudy,
                StartDate = DateTime.Now

            };
            responsesList.Add(promoteStudentResponse);
            return responsesList;


        }
    } 
}
