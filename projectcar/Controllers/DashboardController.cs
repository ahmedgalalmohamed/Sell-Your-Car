using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectcar.Models;

namespace projectcar.Controllers
{
    public class DashboardController : Controller
    {
        CarContext carContext;

        public DashboardController(CarContext carContext)
        {
            this.carContext = carContext;
        }

        public IActionResult Index()
        {
            string username = HttpContext.Session.GetString("username");
            if (username != null)
            {
                UserModel? userModel = carContext.Users.SingleOrDefault(u => u.Username == username);
                return View(userModel);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profile(bool x)
        {

            string username = HttpContext.Session.GetString("username");
            if (username != null)
            {
                if (x)
                {
                    UserModel? userModel = carContext.Users.SingleOrDefault(u => u.Username == username);
                    return View(userModel);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public string UpdateUser(UserModel user, IFormFile fileinp)
        {

            try
            {
                UserModel olduser = carContext.Users.SingleOrDefault(u => u.Username == user.Username);
                if (user.Password == null)
                {
                    if (fileinp != null)
                    {
                        string exten = fileinp.FileName.Substring(fileinp.FileName.IndexOf('.') + 1);

                        string newpath = "";

                        string oldpath = olduser.Img;

                        FileInfo file = new FileInfo("wwwroot" + oldpath);
                        if (file.Exists && !oldpath.Contains("def_"))
                        {
                            file.Delete();
                        }

                        if (!oldpath.Contains("_") || oldpath.Contains("def_"))
                        {
                            newpath = $"/images/User/'{olduser.Username}_'.{exten}";
                        }
                        else
                        {
                            newpath = $"/images/User/'{olduser.Username}'.{exten}";
                        }
                        FileStream fileStream = new FileStream("wwwroot" + newpath, FileMode.Create);
                        fileinp.CopyTo(fileStream);
                        fileStream.Close();
                        olduser.Img = newpath;
                    }
                    olduser.Address = user.Address;
                    olduser.CompanyName = user.CompanyName;
                    olduser.fname = user.fname;
                    olduser.lname = user.lname;

                }
                else
                {
                    olduser.Password = user.Password;
                }
                carContext.SaveChanges();
                return olduser.Img;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        //Manage Role for User
        public IActionResult SystemAdmin(bool x)
        {
            string role = HttpContext.Session.GetString("role");
            if (role == "system admin")
            {
                if (x)
                {
                    List<UserModel> userModels = carContext.Users.Include(u => u.Cars).ToList();
                    return View(userModels);
                }
            }
            return RedirectToAction("Index", "Dashboard");
        }

        //function help me
        [HttpPost]
        public bool OldPassword(string oldpassword)
        {
            string username = HttpContext.Session.GetString("username");
            if (username != null)
            {
                UserModel? userModel = carContext.Users.SingleOrDefault(u => u.Username == username);
                if (userModel.Password == oldpassword)
                {
                    return true;
                }
            }
            return false;
        }
        [HttpPost]
        public bool ChangeRole(string username)
        {
            UserModel user = carContext.Users.SingleOrDefault(u => u.Username == username);
            if (user.role == "user")
            {
                user.role = "admin";
            }
            else
            {
                user.role = "user";
            }
            carContext.SaveChanges();
            return true;
        }
        public bool ChangeState(string username)
        {
            UserModel user = carContext.Users.SingleOrDefault(u => u.Username == username);
            if (user.state == "run")
            {
                user.state = "pause";
            }
            else
            {
                user.state = "run";
            }
            carContext.SaveChanges();
            return true;
        }
    }
}
