using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cumulative1.Models;

namespace cumulative1.Controllers
{
    public class ClassesController : Controller
    {
        // GET: Classess
        public ActionResult Index()
        {
            return View();
        }
        //GET : /Class/List
        public ActionResult List()
        {
            ClassesDataController controller = new ClassesDataController();
            IEnumerable<Classes> Classes = controller.ListClasses();
            return View(Classes);
        }

        //GET : /Author/Show/{id}
        public ActionResult Show(int id)
        {
            ClassesDataController controller = new ClassesDataController();
            Classes NewClass = controller.FindClasses(id);


            return View(NewClass);
        }
    }
}