using cw10.DTOs;
using cw10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw10.Services
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
        public Student ModifyStudent(Student student);
        public Student DeleteStudent(String IndexNumber);
        public Student CreateStudent(Student student);
        public IEnumerable<PromoteStudentResponse> PromoteStudent(PromoteStudentRequest psr);
    }
}
