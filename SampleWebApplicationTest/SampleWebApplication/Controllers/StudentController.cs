using SampleWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static SampleWebApplication.DataAccess.StudentDBHandler;

namespace SampleWebApplication.Controllers
{
    public class StudentController : ApiController
    {

        [HttpGet]
        public IHttpActionResult Index()
        {
            StudentDBHandle dbhandle = new StudentDBHandle();
            var students = dbhandle.GetStudent();
            return Ok(students);
        }

        [HttpPost]
        public IHttpActionResult Edit([FromUri] int id,[FromBody] StudentModel smodel)
        {
            try
            {
                StudentDBHandle sdb = new StudentDBHandle();
                smodel.Id = id;
                if (sdb.UpdateDetails(smodel))
                {
                    return Ok("Student Details Updated Successfully");
                }

                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult Create(StudentModel smodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StudentDBHandle sdb = new StudentDBHandle();
                    if (sdb.AddStudent(smodel))
                    {
                        return Ok("Student Details Added Successfully");
                    }
                }
                return BadRequest(ModelState);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                StudentDBHandle sdb = new StudentDBHandle();
                if (sdb.DeleteStudent(id))
                {
                    return Ok("Student Record Deleted Successfully");
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
