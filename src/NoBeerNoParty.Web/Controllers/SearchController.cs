using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace NoBeerNoParty.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IOptions<Settings> _settings;
        public SearchController(IOptions<Settings> settings)
        {
            _settings = settings;
        }

        public IActionResult Index()
        {
            ViewBag.SearchKey = _settings.Value.SearchQuerykey;
            ViewBag.SearchResourceName = _settings.Value.SearchResourceName;
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }

    }
}