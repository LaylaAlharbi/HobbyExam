using HobbyExam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HobbyExam.Controllers
{
    public class HobbyController : Controller
    {
        private MyContext _db;
        private int? uid
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
        }
        private bool isLoggedIn
        {
            get { return uid != null; }
        }

        public HobbyController(MyContext context)
        {
            _db = context;
        }
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Hobby> allHobby = _db.hobbies   
                .Include(m => m.PostedBy)
                .Include(m => m.Fans)  
                .ToList();
            
            User u = _db.Users.FirstOrDefault(u => u.UserId == (int)uid);
            ViewBag.User = u;
            return View(allHobby);
        }
        [HttpGet("hobbies/new")]
        public IActionResult NewHobby()
        {
            User u = _db.Users.FirstOrDefault(u => u.UserId == (int)uid);
            ViewBag.User = u;
            return View();
        }
        [HttpPost("posthobby")]
        public IActionResult PostHobby(Hobby hobby)
        {
            if (ModelState.IsValid)
            {
                if (_db.hobbies.Any(u => u.Name == hobby.Name))
                {
                    ModelState.AddModelError("Name", "The Name already in use!");
                    return View("NewHobby");
                }
                hobby.UserId = (int)uid;
                _db.hobbies.Add(hobby);
                _db.SaveChanges();
                return Redirect("dashboard");
            }
            User u = _db.Users.FirstOrDefault(u => u.UserId == (int)uid);
            ViewBag.User = u;
            return View("NewHobby");
        }

        [HttpGet("hobbies/{hobbyId}")]
        public IActionResult HobbyDet(int hobbyId)
        {
            // query the movie by id
            Hobby thisHobby = _db
            .hobbies
            .Include(m => m.PostedBy)
            .Include(m => m.Fans)
            .ThenInclude(f => f.Fan)
            .FirstOrDefault(m => m.HobbyId == hobbyId);
            User u = _db.Users.FirstOrDefault(u => u.UserId == (int)uid);
            ViewBag.User = u;
            return View(thisHobby);
        }
        [HttpGet("delete/{hobbyId}")]  
        public IActionResult Delete(int hobbyId)
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            Hobby delHobby = _db.hobbies.FirstOrDefault(m => m.HobbyId == hobbyId);
            _db.hobbies.Remove(delHobby);
            _db.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet("edit/{hobbyId}")]
        public IActionResult Edit(int hobbyId)
        {
            User g = _db.Users.FirstOrDefault(g => g.UserId == (int)uid);
            ViewBag.User = g;
            Hobby hobby = _db.hobbies.FirstOrDefault(w => w.HobbyId == hobbyId);
            return View(hobby);
        }
        [HttpPost("updatehobby/{hobbyId}")]
        public IActionResult UpdateHobby(Hobby hobby, int hobbyId)
        {        
            if (ModelState.IsValid)
            {

                if (_db.hobbies.Any(u => u.Name == hobby.Name))
                {
                    ModelState.AddModelError("Name", "The Name already in use!");
                    return View("Edit", hobby);
                }

                Hobby hobbyFromDB = _db.hobbies.FirstOrDefault(w => w.HobbyId == hobbyId);
                hobbyFromDB.Name = hobby.Name;
                hobbyFromDB.Description = hobby.Description;
                _db.SaveChanges();
                Console.WriteLine("successfully updated");
                return Redirect($"/hobbies/{hobby.HobbyId}");
            }
            User g = _db.Users.FirstOrDefault(g => g.UserId == (int)uid);
            ViewBag.User = g;
            Console.WriteLine("There were some errors, should see errors");
            return View("Edit", hobby);
            }


        [HttpGet("like/{hobbyId}")]
        public IActionResult Like(int hobbyId)
        {
            Like like = new Like();
            like.UserId = (int)uid;
            like.HobbyId = hobbyId;
            _db.Likes.Add(like);
            _db.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet("unlike/{hobbyId}")]
        public IActionResult Unlike(int hobbyId)
        {
            Like unlike = _db.Likes.FirstOrDefault(l => l.FanOf.HobbyId == hobbyId && l.Fan.UserId == (int)uid);
            _db.Likes.Remove(unlike);
            _db.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}