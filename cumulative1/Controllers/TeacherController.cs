using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cumulative1.Models;
using System.Diagnostics;

namespace cumulative1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        //GET : /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);


            return View(SelectedTeacher);
        }

        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }

        //POST : /Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }
        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }
        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname,string TeacherLname,string EmployeeNumber,DateTime HireDate,string Salary)
        {  //Identify that this method is running
            //Identify the inputs provided fromthe form
            Debug.WriteLine("I have accessed the create Method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmployeeNumber);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(Salary);
            //create a new teaher object

            Teacher NewTeacher = new Teacher();

            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);
            

            return RedirectToAction("List");
        }

        //GET : /Teacher/Update/{id}
        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page.Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current information of the teacher and asks the user for new information as part of a form</returns>
        /// <exampleGET : /Teacher/Update/{id}</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        //POST : /Teacher/Update/{id}
        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the  system,with new values.Conveys this informationto the API,and redirects to the "Teacher Show"page of our updated teacher.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the teacher</param>
        /// <param name="TeacherLname">The updated last name of the teacher<</param>
        /// <param name="EmployeeNumber">The updated EmployeeNumber of the teacher</param>
        /// <param name="HireDate">The updated HireDate of the teacher</param>
        /// <param name="Salary">The updated Salary of the teacher</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher</returns>
        /// <example>
        /// POST : /Teacher/Update/{id}
        /// POST : /Teacher/Update/2
        /// {
        /// "TeacherFname" : "CATALIN",
        /// "TeacherLname" : "Cummings",
        /// "EmployeeNumber" : "T381",
        /// "HireDate" : "19-04-2024",
        /// "Salary" :"62.77",
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id,string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, string Salary)
        {
            Teacher TeacherInfo = new Teacher();

            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.EmployeeNumber = EmployeeNumber;
            TeacherInfo.HireDate = HireDate;
            TeacherInfo.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id,TeacherInfo);
            return RedirectToAction("Show/" + id);
        }
    }
}