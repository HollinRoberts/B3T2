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
    public class IdeaController : Controller
    {
        private B3T2Context _context;
         public IdeaController(B3T2Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("activity")]
        public IActionResult activity()
        {   
            if(ViewBag.errors==null)
            {
            List<Object> error = new List<object>();
            ViewBag.errors= error;
            }
            int? id = HttpContext.Session.GetInt32("userId");
            string name = HttpContext.Session.GetString("name");
            if(id!=null){
            List<Ideas> AllIdeas = new List<Ideas>();
           
            AllIdeas = _context.ideas.Include(user=>user.author).Include(likes=>likes.liked).ThenInclude(user=>user.User).OrderByDescending(d=>d.liked.Count).ToList();
            ViewBag.AllIdeas=AllIdeas;
            ViewBag.User=(int)id;
            ViewBag.Name=name;


            return View("activity");
            }
            return RedirectToAction("index","login");

        }
    //     [HttpGet]
    //     [Route("new")]
    //     public IActionResult New()
    //     {
    //         int? id = HttpContext.Session.GetInt32("userId");
    //         if(id!=null){
    //             List<Object> error = new List<object>();
    //             ViewBag.errors= error;
    //             return View("new");
    //         }
    //         return RedirectToAction("index","login");
    //     }
        [HttpPost]
        [Route("create")]
        public IActionResult create(IdeasValidate idea1)
        {
            int? id = HttpContext.Session.GetInt32("userId");
            if (TryValidateModel(idea1)){
                
                Ideas nw= new Ideas
                    {
                        idea = idea1.idea,
                        userid = (int)id,
                        
                    };
                    _context.Add(nw);
                    _context.SaveChanges();
                    
                    TempData["id"]=nw.ideasid;
                    return RedirectToAction("activity");
                }else{
                    ViewBag.errors = ModelState.Values;
                    return View("activity");
                
            }
        
        }
        [HttpGet]
        [Route("bright_ideas/{id}")]
        public IActionResult details(int id)
        {  
       
            int? userid = HttpContext.Session.GetInt32("userId");
            if(userid!=null){
                Ideas Details = _context.ideas.Include(user=>user.author).Include(attending=>attending.liked).ThenInclude(user=>user.User).SingleOrDefault(detail=>detail.ideasid==id);   
                ViewBag.Details=Details;
                // List<Liked> error = new List<object>();
                List<Liked> AllLikes = _context.liked.Where(like=>like.ideasid == id).Include(user=>user.User).ToList();
                var groupedlikes = from Liked in AllLikes
                group Liked by Liked.User into g
                select g;
                ViewBag.AllLikes=groupedlikes;
                // List<Liked> AllLikes = new List<Liked>();
                // AllLikes = _context.liked.GroupBy(likes=>likes.userid).ToList();
                // Other code
                ViewBag.User=(int)userid;
                return View("details");
                
                }
            return RedirectToAction("index","login");
        }
        [HttpGet]
        [Route("users/{id}")]
        public IActionResult users(int id)
        {  
       
            int? userid = HttpContext.Session.GetInt32("userId");
            if(userid!=null){
                User Details = _context.user.Include(user=>user.liked).Include(ideas=>ideas.ideas).SingleOrDefault(detail=>detail.userid==id);   
                ViewBag.Details=Details;
                // List<Liked> AllLikes = new List<Liked>();
                // AllLikes = _context.liked.GroupBy(likes=>likes.userid).ToList();
                // Other code
                ViewBag.User=(int)userid;
                return View("users");
                
                }
            return RedirectToAction("index","login");
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult logout(int id)
        {
            HttpContext.Session.Clear();
            // Other code
            return RedirectToAction("index","login");
        }
        [HttpGet]
        [Route("like/{id}")]
        public IActionResult like(int id)
        {
            int? uid = HttpContext.Session.GetInt32("userId");
            Liked nw= new Liked
                    {
                        userid = (int)uid,
                        ideasid = id,
                        
                    };
                    _context.Add(nw);
                    _context.SaveChanges();
            // Other code
            return RedirectToAction("activity","idea");
        }
  
        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult delete(int id)
        {
            Ideas toremove = _context.ideas.SingleOrDefault(detail=>detail.ideasid==id);   
            _context.ideas.Remove(toremove);
            _context.SaveChanges();
            // Other code
            return RedirectToAction("activity","idea");
        }
    }
}
