using IServices;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace ThreeLayersFrameTemplates.Controllers
{
    public class HomeController : Controller
    {
        public ILog log { get; set; }
        IPersonsService _personService;

        public HomeController(IPersonsService personService)
        {
            _personService = personService;
        }

        public async Task<ActionResult> Index()
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";

            //查询
            var query = _personService.Query(c => true);

            if (!query.Any())
            {
                _personService.Add(new Models.Persons
                {
                    Id = Guid.NewGuid(),
                    Name = "Loongle",
                    Age = 18
                });

                await _personService.SaveChangesAsync();
            }

            query = _personService.Query(c => true);

            ViewData["query"] = query;

            log.Debug(DateTime.UtcNow.ToString());

            return View();
        }
    }
}
