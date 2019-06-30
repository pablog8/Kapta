using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kapta.Backend.Models;
using Kapta.Common.Models;

namespace Kapta.Backend.Controllers
{
    public class ExercisessController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Exercisess
        public async Task<ActionResult> Index()
        {
            return View(await db.Exercises.ToListAsync());
        }

        // GET: Exercisess/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercise exercise = await db.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // GET: Exercisess/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exercisess/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ExerciseId,Name,Description,ImagePath,PublishOn")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                db.Exercises.Add(exercise);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(exercise);
        }

        // GET: Exercisess/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercise exercise = await db.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // POST: Exercisess/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ExerciseId,Name,Description,ImagePath,PublishOn")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exercise).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(exercise);
        }

        // GET: Exercisess/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercise exercise = await db.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // POST: Exercisess/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Exercise exercise = await db.Exercises.FindAsync(id);
            db.Exercises.Remove(exercise);
            await db.SaveChangesAsync();
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
