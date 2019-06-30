namespace Kapta.API.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Common.Models;
    using Domain.Models;

    [Authorize]
    public class VideosController : ApiController
    {
        private DataContext db = new DataContext();

        public IQueryable<Video> GetVideos()
        {
            return db.Videos.OrderBy(c => c.NombreVideo);
        }
    }

}