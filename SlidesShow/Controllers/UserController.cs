using Microsoft.AspNetCore.Mvc;
using SlidesShow.DTOs;
using SlidesShow.Models;
using System.Security.Cryptography;

namespace SlidesShow.Controllers
{
    public class UserController : Controller
    {
        private readonly SlidesDbContext _db;

        public UserController(SlidesDbContext db)
        {
            _db = db;
            
        }
        public IActionResult LoginView()
        {
            HttpContext.Session.SetInt32("loggedIn", 0);
            return View();
        }


        [HttpPost]
        public IActionResult LoginView(LoginDetails loginDetails)
        {
            var users = _db.Users.ToList();
            if (users.Count() == 0 || loginDetails == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var user = users.FirstOrDefault(u => u.Email == loginDetails.Email);

                    if (user == null)
                    {
                        ViewBag.loginError = "User not Exists";
                        return View();
                    }

                    var currentUser = _db.Users.FirstOrDefault(u => u.Email == loginDetails.Email && u.Password == loginDetails.Password);
                    if (currentUser != null)
                    {
                        HttpContext.Session.SetInt32("loggedIn", 1);
                        HttpContext.Session.SetInt32("userId",currentUser.Id);
                        return RedirectToAction("Index","Company");
                    }
                    else
                    {
                        ViewBag.loginError = "Invalid Credentials!!";
                        return View();

                    }


                }
                catch
                {
                    throw;
                }
            }

            return View();
        }

        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public IActionResult Register(Register newUser)
        {
            var user = _db.Users.FirstOrDefault(u=>u.Email == newUser.Email);

            if (user!=null)
            {
                ViewBag.message = " User already exists";
                return View("Register");
            }

            User createUser = new User
            {
                Email = newUser.Email,
                UserName = newUser.UserName,
                PhoneNumber = newUser.PhoneNumber,
                Password = newUser.Password,

            };

            _db.Users.Add(createUser);
            int result = _db.SaveChanges();

            bool isValidUser = true;
            ViewBag.Registered = "Registration Successful!! Please login to continue";
            ViewBag.ValidUser = isValidUser;
            HttpContext.Session.SetInt32("loggedIn", 0);

            return View("LoginView");
        }


        public IActionResult LogOut()
        {
            HttpContext.Session.SetInt32("loggedIn", 0);

            return View("LoginView");
        }

    }
}
