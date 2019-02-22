using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web.Context;
using web.Models;

namespace web.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly RSSContext _rssContext;

        public ApiController(RSSContext rSSContext)
        {
            this._rssContext = rSSContext;
        }

        [Route("rss")]
        public JsonResult RSS()
        {
            return Json(_rssContext.Set<RSS>().AsNoTracking());
        }
    }
}
