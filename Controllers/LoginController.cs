using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using B3T2.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace B3T2.Controllers
{
    public class LoginController : Controller
    {
        // GET: /Home/
        private B3T2Context _context;
 
        public LoginController(B3T2Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult index()
        {   
            List<Object> error = new List<object>();
            ViewBag.errors= error;
            return View("login");
        }
        [HttpPost]
        [Route("submit")]
    
        public IActionResult submit(regvalidate NewUser)
        {
            if (ModelState.IsValid)
            {   User User1 =_context.user.SingleOrDefault(user=>user.email==NewUser.email);
                PasswordHasher<regvalidate> Hasher = new PasswordHasher<regvalidate>();
                NewUser.password = Hasher.HashPassword(NewUser, NewUser.password);
                if( User1==null){
                    User New= new User
                    {
                        name = NewUser.name,
                        alias = NewUser.alias,
                        email = NewUser.email,
                        password = NewUser.password,
                    };
                    _context.Add(New);
                    _context.SaveChanges();
                HttpContext.Session.SetInt32("userId", New.userid);
                HttpContext.Session.SetString("name", New.name);
                ViewBag.Name=New.name;
                ViewBag.User=New.userid;
                   return RedirectToAction("activity","idea");
                }else{
                    ModelState.AddModelError("RegisterFail", "User already exists.");
                    ViewBag.errors = ModelState.Values;
                    return View("login");
                    }
                }
            
            else
            {
                Console.WriteLine("in else");
             
                ViewBag.errors = ModelState.Values;
            
                Console.WriteLine(ViewBag.errors);
                return View("login");
            }
           
        }
        [HttpPost]
        [Route("login")]
        public IActionResult login(Login userLogin )
        {   
            if (ModelState.IsValid)
            {
             User User1 = _context.user.SingleOrDefault(user=>user.email==userLogin.Email);
                
                if(User1!=null){
                    var Hasher = new PasswordHasher<User>();
                    if(User1.email==userLogin.Email &&  (0 != Hasher.VerifyHashedPassword(User1, User1.password, userLogin.Password))){
                        HttpContext.Session.SetInt32("userId", User1.userid);
                        HttpContext.Session.SetString("name", User1.name);
                        ViewBag.Name=User1.name;
                        ViewBag.User=User1.userid;
                        
                  
                        return RedirectToAction("activity","idea");
                     }else{
                        ModelState.AddModelError("LogFail", "Invalid Login");
                        ViewBag.errors = ModelState.Values;
                        Console.WriteLine(ViewBag.errors);
                         return View("login");
                     }
                }else{
                    ModelState.AddModelError("LogFail", "Invalid Login");
                    ViewBag.errors = ModelState.Values;
                    return View("login");
                }
            }
             else
            {
                Console.WriteLine("in else");
             
                ViewBag.errors = ModelState.Values;
            
                Console.WriteLine(ViewBag.errors);
                return View("login");
            }
        }
        // [HttpPost]
        // [Route("action")]
        // public IActionResult update(int value)
        // {   
           
        // int? id = HttpContext.Session.GetInt32("userId");
        // User Account = _context.user.Include(users=>users.wedding).SingleOrDefault(user=>user.userid==(int)id);

        // // Activity action= new Activity
        // //             {
        // //                 userid = (int)id,
        // //                 change = value,
                        
        // //             };
        // //             _context.Add(action);
        // //             _context.SaveChanges();
        // //        Account.ballance=Account.ballance+value;
        // //        _context.SaveChanges();
        // //         ViewBag.Name=Account.first;
        // //         ViewBag.Ballance=Account.ballance;
        // // ViewBag.Transactions=Account.activity;
        //         return View("account");
            
        // }

    }
}
 