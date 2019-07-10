namespace Kapta.Backend.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Common.Models;
    using Helpers;
    using Models;

    [Authorize(Users = "pablo.kapta@gmail.com")]
    public class VideosController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        public async Task<ActionResult> Index()
        {
            return View(await this.db.Videos.OrderBy(c => c.NombreVideo).ToListAsync());
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VideoView view)
        {
            if (ModelState.IsValid)
            {
                var pic = string.Empty;
                var folder = "~/Content/Videos";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }

                var video = this.ToVideo(view, pic);
                this.db.Videos.Add(video);
                await this.db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        private Video ToVideo(VideoView view, string pic)
        {
            return new Video
            {
                VideoId = view.VideoId,
                NombreVideo = view.NombreVideo,
                Description = view.Description,
                LinkVideo = view.LinkVideo,
                ImagePath = pic,
            };
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var video = await this.db.Videos.FindAsync(id);

            if (video == null)
            {
                return HttpNotFound();
            }

            var view = this.ToView(video);
            return View(view);
        }

        private VideoView ToView(Video video)
        {
            return new VideoView
            {
                VideoId = video.VideoId,
                NombreVideo = video.NombreVideo,
                Description = video.Description,
                LinkVideo = video.LinkVideo,
                ImagePath = video.ImagePath,
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VideoView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.ImagePath;
                var folder = "~/Content/Videos";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }

                var video = this.ToVideo(view, pic);
                this.db.Entry(video).State = EntityState.Modified;
                await this.db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(view);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var video = await this.db.Videos.FindAsync(id);

            if (video == null)
            {
                return HttpNotFound();
            }

            return View(video);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var video = await this.db.Videos.FindAsync(id);
            this.db.Videos.Remove(video);
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