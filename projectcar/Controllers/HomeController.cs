using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectcar.Models;
using projectcar.ViewModel;
using System.Diagnostics;

namespace projectcar.Controllers
{
    public class HomeController : Controller
    {
        CarContext carContext;
        public HomeController(CarContext carContext)
        {
            this.carContext = carContext;
        }

        public IActionResult Index()
        {
            ListBodyStyleVM bodyStyleVM = new ListBodyStyleVM(carContext)
            {
                cars = carContext.Cars.Where(c=>!c.Img.Contains("def_car")).ToList(),
                bodyStyles = carContext.BodyStyles.ToList()
            };
            return View(bodyStyleVM);
        }
        public IActionResult Detials(string name)
        {
            ViewData["link"] = name;
            ViewData["action"] = "Index";
            ViewData["controller"] = "Home";
            BodyStyleModel? bodyStyle = carContext.BodyStyles.Where(bs => bs.Name == name).Include(m => m.Models).ThenInclude(m => m.Maker).Include(bs => bs.Models).ThenInclude(m => m.Cars).SingleOrDefault();
            if (bodyStyle != null)
            {
                HomeDetialsVM homeDetials = new HomeDetialsVM()
                {
                    bodyStyle = bodyStyle,
                    maker = carContext.Makers.Include(m => m.Models).ToList()
                };
                return View(homeDetials);
            }
            return View("myError", new ErrorModel() { MessageError = "This Body Style Not Found " + "( " + name + " )" });

        }

        public IActionResult AboutUs()
        {
            ViewData["link"] = "About Us";
            ViewData["action"] = "AboutUs";
            ViewData["controller"] = "Home";
            return View();
        }
        [HttpGet]
        public IActionResult AdvancedSearch()
        {
            ViewData["link"] = "Advanced Search";
            ViewData["action"] = "AdvancedSearch";
            ViewData["controller"] = "Home";
            CarUserVM carUser = new CarUserVM()
            {
                cars = carContext.Cars.Include(c => c.Model).ThenInclude(m => m.Maker).Include(m => m.Model).ThenInclude(m => m.BodyStyle).ToList(),
                BodyStyles = carContext.BodyStyles.ToList(),
                Makers = carContext.Makers.ToList()
            };
            return View(carUser);
        }

        [HttpPost]
        public IActionResult AdvancedSearch(int model)
        {
            ViewData["link"] = "Advanced Search";
            ViewData["action"] = "AdvancedSearch";
            ViewData["controller"] = "Home";
            CarUserVM carUser = new CarUserVM()
            {
                cars = carContext.Cars.Where(c=>c.ModelId == model).Include(c => c.Model).ThenInclude(m => m.Maker).Include(m => m.Model).ThenInclude(m => m.BodyStyle).ToList(),
                BodyStyles = carContext.BodyStyles.ToList(),
                Makers = carContext.Makers.ToList()
            };
            return View(carUser);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //function help me

    }
}