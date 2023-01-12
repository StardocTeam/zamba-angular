using Kendo.Mvc.Examples.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class MediaPlayerController : Controller
    {
        [Demo]
        public ActionResult Playlist()
        {
            return View(GetVideos());
        }

        public ActionResult Videos_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetVideos().ToDataSourceResult(request));
        }

        private static IEnumerable<Video> GetVideos()
        {
            List<Video> videos = new List<Video>();

            videos.Add(new Video()
            {
                title = "Telerik Platform - Enterprise Mobility. Unshackled.",
                poster = "http://img.youtube.com/vi/N3P6MyvL-t4/1.jpg",
                source = "https://www.youtube.com/watch?v=N3P6MyvL-t4"
            });
            videos.Add(new Video()
            {
                title = "Learn How York Solved Its Database Problem",
                poster = "http://img.youtube.com/vi/_S63eCewxRg/1.jpg",
                source = "https://www.youtube.com/watch?v=_S63eCewxRg"
            });
            videos.Add(new Video()
            {
                title = "Responsive Website Delivers for Reeves Import Motorcars",
                poster = "http://img.youtube.com/vi/DYsiJRmIQZw/1.jpg",
                source = "https://www.youtube.com/watch?v=DYsiJRmIQZw"
            });
            videos.Add(new Video()
            {
                title = "Digital Transformation: A New Way of Thinking",
                poster = "http://img.youtube.com/vi/gNlya720gbk/1.jpg",
                source = "https://www.youtube.com/watch?v=gNlya720gbk"
            });
            videos.Add(new Video()
            {
                title = "Take a Tour of the Telerik Platform",
                poster = "http://img.youtube.com/vi/rLtTuFbuf1c/1.jpg",
                source = "https://www.youtube.com/watch?v=rLtTuFbuf1c"
            });
            videos.Add(new Video()
            {
                title = "Why Telerik Analytics - Key Benefits for your applications",
                poster = "https://i.ytimg.com/vi/CpHKm2NruYc/1.jpg",
                source = "https://www.youtube.com/watch?v=CpHKm2NruYc"
            });
            return videos;
        }
    }
}
