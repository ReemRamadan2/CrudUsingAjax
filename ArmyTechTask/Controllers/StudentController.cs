using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArmyTechTask;

namespace ArmyTechTask.Controllers
{
    public class StudentController : Controller
    {
        ArmyTechTask db = new ArmyTechTask();

        // GET: Student
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Field).Include(s => s.Governorate).Include(s => s.Neighborhood).Include(a=>a.StudentTeachers);
            return View(students.ToList());
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            ViewBag.FieldId = new SelectList(db.Fields, "ID", "Name");
            ViewBag.GovernorateId = new SelectList(db.Governorates, "ID", "Name");
            ViewBag.NeighborhoodId = new SelectList(db.Neighborhoods, "ID", "Name");
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,BirthDate,GovernorateId,NeighborhoodId,FieldId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FieldId = new SelectList(db.Fields, "ID", "Name", student.FieldId);
            ViewBag.GovernorateId = new SelectList(db.Governorates, "ID", "Name", student.GovernorateId);
            ViewBag.NeighborhoodId = new SelectList(db.Neighborhoods, "ID", "Name", student.NeighborhoodId);
            return View(student);
        }

        // GET: Student/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            StudentTeacher studentTeacher = (StudentTeacher) db.StudentTeachers.SingleOrDefault(a => a.StudentId == id);


            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.FieldId = new SelectList(db.Fields, "ID", "Name", student.FieldId);
            ViewBag.GovernorateId = new SelectList(db.Governorates, "ID", "Name", student.GovernorateId);
            ViewBag.NeighborhoodId = new SelectList(db.Neighborhoods, "ID", "Name", student.NeighborhoodId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "ID", "Name", studentTeacher!=null? studentTeacher.TeacherId:0);
            return View(student);
        }

        // POST: Student/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,BirthDate,GovernorateId,NeighborhoodId,FieldId")] Student student)
        {
            //  StudentTeacher studentTeacher = (StudentTeacher)db.StudentTeachers.SingleOrDefault(a => a.TeacherId== TeacherId);  
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;

                //db.Entry(studentTeacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FieldId = new SelectList(db.Fields, "ID", "Name", student.FieldId);
            ViewBag.GovernorateId = new SelectList(db.Governorates, "ID", "Name", student.GovernorateId);
            ViewBag.NeighborhoodId = new SelectList(db.Neighborhoods, "ID", "Name", student.NeighborhoodId);
           // ViewBag.TeacherId = new SelectList(db.Teachers, "ID", "Name", studentTeacher != null ? studentTeacher.TeacherId : 0);
            ViewBag.NeighborhoodId = new SelectList(db.Neighborhoods, "ID", "Name", student.NeighborhoodId);

            return View(student);
        }
        [HttpPost]
        public JsonResult addTeachertostudent(int TeacherId,int StudentId)
        {
            StudentTeacher Studentteacher= db.StudentTeachers.SingleOrDefault(a => a.StudentId == StudentId);
            if (Studentteacher == null)
            {
                StudentTeacher studentTeacher0 =new StudentTeacher()
                { 
                    TeacherId = TeacherId,
                    StudentId = StudentId
                };
                
                db.StudentTeachers.Add(Studentteacher);
                db.SaveChanges();
                return Json(new { Response = "Added" }, JsonRequestBehavior.AllowGet);

            }
            if (Studentteacher.TeacherId== TeacherId || TeacherId ==0)
            {
                     return Json(new { Response = "Already exist  " },JsonRequestBehavior.AllowGet);
            }
            else
            {
                Studentteacher.TeacherId = TeacherId;
                db.Entry(Studentteacher).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(new { Response = "response " },JsonRequestBehavior.AllowGet);
        }

        // GET: Student/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
