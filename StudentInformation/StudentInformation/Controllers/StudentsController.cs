using Microsoft.AspNetCore.Mvc;
using StudentInformation.Data;
using StudentInformation.Models;

namespace StudentInformation.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentInformationContext _context;

        public StudentsController(StudentInformationContext context)
        {
            _context = context;
        }

        public ActionResult Index(string searchString, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["ResultSortParm"] = String.IsNullOrEmpty(sortOrder) ? "result_desc" : "";

            IQueryable<Student> students = from s in _context.Students
                                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(student => student.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(student => student.Name);
                    break;
                case "result_desc":
                    students = students.OrderByDescending(student => student.Result);
                    break;
                default:
                    students = students.OrderBy(students => students.Name);
                    break;
            }

            return View(students.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreateConfirmed(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        public ActionResult Edit(int id)
        {
            Student? student = _context.Students.Find(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(int id, Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Update(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        public ActionResult Delete(int id)
        {
            Student? student = _context.Students.Find(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Student? student = _context.Students.Find(id);

            if (student != null)
            {
                _context.Remove(student);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
