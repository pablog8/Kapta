namespace Kapta.Backend.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using Common.Models;

    public class VideoView : Video
    {
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}