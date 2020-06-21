using cw10.Models;
using cw10.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw10.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        public IDbService _service;

        public StudentsController(IDbService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetStudents()
        {

            var res = _service.GetStudents();
            return Ok(res);

        }
        [Route("api/update")]
        [HttpPost]

        public IActionResult ModifyStudent(Student _student)
        {
            var res = _service.ModifyStudent(_student);
            return Ok(res);
        }

        [HttpDelete("{IndexNumber}")]

        public IActionResult DeleteStudent(String IndexNumber)
        {
            var res = _service.DeleteStudent(IndexNumber);
            return Ok(res);
        }

        [Route("api/create")]
        [HttpPost]

        public IActionResult CreateStudent(Student _student)
        {
            var res = _service.CreateStudent(_student);
            return Ok(res);

        }

    }
}