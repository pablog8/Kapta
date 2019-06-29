namespace Kapta.Backend.Controllers
{
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
    using Kapta.Backend.Helpers;

    [Authorize]
    public class ExercisesController : Controller
    {
        //la base de datos es db
        private LocalDataContext db = new LocalDataContext();

        // GET: Exercises
        //devuelve una vista con la lista de todos los ejercicios
        public async Task<ActionResult> Index()
        {
            return View(await this.db.Exercises.OrderBy(p => p.Description).ToListAsync());
        }

        // GET: Exercises/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var exercise = await this.db.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // GET: Exercises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exercises/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ExerciseView view)
        {
            if (ModelState.IsValid)
            {
                //preguntamos si hay imagen para subirla al servidor
                var pic = string.Empty;
                var folder = "~/Content/Exercises";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }

                //convertimos el objeto productsview a product, que posteriormente mandamos a la base de datos
                var exercise = this.ToExercise(view, pic);

                this.db.Exercises.Add(exercise);
                await this.db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(view);
        }
        private Exercise ToExercise(ExerciseView view, string pic)
        {
            return new Exercise
            {
                Name = view.Name,
                Description = view.Description,
                ImagePath = pic,
                //CategoryId = view.CategoryId,
                //IsAvailable = view.IsAvailable,
                //Price = view.Price,
                ExerciseId = view.ExerciseId,
                PublishOn = view.PublishOn,
                //Remarks = view.Remarks,
            };
        }

        // GET: Exercises/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var exercise = await this.db.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            var view = this.ToView(exercise);
            return View(view);
        }

        private ExerciseView ToView(Exercise exercise)
        {
            return new ExerciseView
            {
                Name = exercise.Name,
                Description = exercise.Description,
                //CategoryId = product.CategoryId,
                ImagePath = exercise.ImagePath,
                //IsAvailable = product.IsAvailable,
                //Price = product.Price,
                ExerciseId = exercise.ExerciseId,
                PublishOn = exercise.PublishOn,
                //Remarks = product.Remarks,
            };
        }

        // POST: Exercises/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ExerciseView view)
        {
            if (ModelState.IsValid)
            {
                //preguntamos si hay imagen para subirla al servidor
                var pic = view.ImagePath;
                var folder = "~/Content/Exercises";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }

                //convertimos el objeto productsview a product, que posteriormente mandamos a la base de datos
                var exercise = this.ToExercise(view, pic);

                this.db.Entry(exercise).State = EntityState.Modified;
                await this.db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(view);
        }

        // GET: Exercises/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var exercise = await this.db.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var exercise = await this.db.Exercises.FindAsync(id);
            this.db.Exercises.Remove(exercise);
            await this.db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
