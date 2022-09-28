using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectcar.Models;

namespace projectcar.Controllers
{
    public class UserController : Controller
    {
        CarContext carContext;
        public UserController(CarContext carContext)
        {
            this.carContext = carContext;
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                ViewData["link"] = "SignUP";
                ViewData["action"] = "Create";
                ViewData["controller"] = "User";
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Create(UserModel user, IFormFile Img)
        {

            ViewData["action"] = "Create";
            ViewData["controller"] = "User";
            try
            {
                if (Img == null)
                {
                    if (user.Gender == "male")
                    {
                        user.Img = "/images/User/def_m.jpg";
                    }
                    else
                    {
                        user.Img = "/images/User/def_f.jpg";
                    }
                }
                else
                {
                    string exten = Img.FileName.Substring(Img.FileName.IndexOf('.') + 1);
                    string path = $"/images/User/'{user.Username}'.{exten}";
                    FileStream fileStream = new FileStream("wwwroot" + path, FileMode.Create);
                    Img.CopyTo(fileStream);
                    fileStream.Close();
                    user.Img = path;
                }
                carContext.Users.Add(user);
                carContext.SaveChanges();
                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                return View("myError", new ErrorModel() { MessageError = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                ViewData["link"] = "Login";
                ViewData["action"] = "Login";
                ViewData["controller"] = "User";
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public string Login(string Email, string Password)
        {
            UserModel? userModel = carContext.Users.SingleOrDefault(u => u.Email == Email && u.Password == Password);
            if (userModel != null)
            {
                if (userModel.state == "run")
                {
                    HttpContext.Session.SetString("username", userModel.Username);
                    HttpContext.Session.SetString("role", userModel.role);
                    return "y";
                }
                return "System Admin Pause Your Account";
            }
            return "This User Not Found";
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                RedirectToAction("Index", "Home");
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AllUsers(string name)
        {
            ViewData["link"] = "AllUsers";
            ViewData["action"] = "AllUsers";
            ViewData["controller"] = "User";
            if (name == null)
            {
                name = " ";
            }
            List<UserModel> users = carContext.Users.Where(u => (u.fname + " " + u.lname).ToLower().Contains(name.ToLower())).Include(u => u.Cars).ToList();
            return View(users);
        }


        [HttpPost]
        public string DeleteUser(string username)
        {
            if (username != null)
            {
                try
                {
                    UserModel user = carContext.Users.Where(u => u.Username == username).Include(u => u.Cars).SingleOrDefault();
                    if (user != null)
                    {
                        foreach (var car in user.Cars)
                        {
                            string path = $"wwwroot{car.Img}";
                            FileInfo file = new FileInfo(path);
                            if (file.Exists)
                            {
                                file.Delete();
                            }
                        }
                        FileInfo file1 = new FileInfo($"wwwroot{user.Img}");
                        if (file1.Exists && !user.Img.Contains("def_"))
                        {
                            file1.Delete();
                        }
                        carContext.Users.Remove(user);
                        carContext.SaveChanges();
                    }
                    return "y";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "n";
        }
        //function help me

        [HttpPost]
        public string chkEmail(string email)
        {
            UserModel user = carContext.Users.SingleOrDefault(u => u.Email == email);
            if (user != null)
            {
                return "y";
            }
            return "n";
        }


        [HttpPost]
        public string chkPhone(string phone)
        {
            UserModel user = carContext.Users.SingleOrDefault(u => u.Phone == phone);
            if (user != null)
            {
                return "y";
            }
            return "n";
        }

        [HttpPost]
        public string chkUsername(string username)
        {
            UserModel user = carContext.Users.SingleOrDefault(u => u.Username == username);
            if (user != null)
            {
                return "y";
            }
            return "n";
        }
    }
}
