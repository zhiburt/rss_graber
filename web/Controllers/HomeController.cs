using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using web.Context;
using web.Models;

namespace web.Controllers
{
    public enum SortType : int
    {
        ByDate = 0,
        ByLink,
        ByTitle
    }

    public class HomeController : Controller
    {
        private readonly RSSContext _rssContext;
        private const int MAX_ELEMENTS_ON_PAGE = 15;

        public HomeController(RSSContext rSSContext)
        {
            this._rssContext = rSSContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ServerRender(SortType sortOrder, string selectOnly, int page = 1)
        {
            var model = _rssContext.Set<RSS>().ToList();
            ViewData["SelectList"] = model.Select(rss => resourceName(rss.LinkToOriginal)).Distinct();
            
            if (!string.IsNullOrEmpty(selectOnly) || !string.IsNullOrEmpty(ViewData["selectOnly"] as string))
            {
                model = model.Select(rss => rss)
                    .Where(r => resourceName(r.LinkToOriginal) == selectOnly)
                    .ToList();
                ViewData["selectOnly"] = selectOnly;
            }

            switch (sortOrder)
            {
                case SortType.ByDate:
                    model = model.OrderBy(rss => rss.Date).ToList();

                    break;
                case SortType.ByLink:
                    model = model.OrderBy(rss => resourceName(rss.LinkToOriginal)).ToList();
                    break;
                case SortType.ByTitle:
                    model = model.OrderBy(rss => rss.Title).ToList();
                    break;
                default:
                    break;
            }

            ViewData["sortOrder"] = (int)sortOrder;

            ViewData["NextPage"] = -1;
            ViewData["PriviousPage"] = -1;
            int devide = model.Count / MAX_ELEMENTS_ON_PAGE;
            if (devide > 0)
            {
                var skiped = model.Skip(((page * MAX_ELEMENTS_ON_PAGE) - MAX_ELEMENTS_ON_PAGE));
                if (skiped.Count() > MAX_ELEMENTS_ON_PAGE)
                {
                    model = skiped.Take(MAX_ELEMENTS_ON_PAGE).ToList();
                    ViewData["NextPage"] = page+1;
                }
                else
                {
                    model = skiped.ToList();
                }

                ViewData["PriviousPage"] = page-1;
            }

            return View(model);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string resourceName(string link)
        {
            var uri = new Uri(link);
            return uri.Host;
        }
    }
}
