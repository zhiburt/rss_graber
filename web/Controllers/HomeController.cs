using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        private const int MAX_ELEMENTS_ON_PAGE = 10;

        public HomeController(RSSContext rSSContext)
        {
            this._rssContext = rSSContext;
        }

        public IActionResult Index()
        {
            return RedirectToAction("ServerRender");
        }

        public IActionResult ServerRender(SortType sortOrder, string selectOnly, int page = 1)
        {
            var model = _rssContext.Set<RSS>().ToList();
            ViewData["SelectList"] = model.Select(rss => resourceName(rss.LinkToOriginal)).Distinct();

            var onlyResorsec = SelectOnlyByResource(model, selectOnly, ViewData["selectOnly"] as string);
            if (onlyResorsec != null)
            {
                model = onlyResorsec;
                ViewData["selectOnly"] = selectOnly;
            }

            model = SortBy(model, sortOrder);
            ViewData["sortOrder"] = (int)sortOrder;

            (model, ViewData["PriviousPage"], ViewData["NextPage"]) =  Pagination(model, page);

            return View(model);
        }

        private (List<RSS> newmodel, int privious, int next) Pagination(List<RSS> model, int page)
        {
            int privious = -1;
            int next = -1;

            int devide = model.Count / MAX_ELEMENTS_ON_PAGE;
            if (devide > 0)
            {
                var skiped = model.Skip(((page * MAX_ELEMENTS_ON_PAGE) - MAX_ELEMENTS_ON_PAGE));
                if (skiped.Count() > MAX_ELEMENTS_ON_PAGE)
                {
                    model = skiped.Take(MAX_ELEMENTS_ON_PAGE).ToList();
                    next = page + 1;
                }
                else
                {
                    model = skiped.ToList();
                }

                privious = page - 1;
            }

            return (model, privious, next);
        }

        private List<RSS> SelectOnlyByResource(List<RSS> model, string selectOnly, string selectOnlyCashe)
        {
            if (!string.IsNullOrEmpty(selectOnly) || !string.IsNullOrEmpty(selectOnlyCashe))
            {
                model = model.Select(rss => rss)
                    .Where(r => resourceName(r.LinkToOriginal) == selectOnly)
                    .ToList();
                return model;
            }

            return null;
        }

        private List<RSS> SortBy(List<RSS> model, SortType sortOrder)
        {
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

            return model;
        }

        public IActionResult ClientRender()
        {
            return View(_rssContext.Set<RSS>().AsNoTracking());
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
