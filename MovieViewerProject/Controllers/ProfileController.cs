using Microsoft.AspNetCore.Mvc;
using MovieViewerProject.Models.Entities;
using MovieViewerProject.Models.ViewModels;
using MovieViewerProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*This is the Profile Controller. This class controls how profile data is returned to the view.
 The Create methods provide a form for you to create yourself a profile, and to add the information into the database.
 The Details method displays information about individual profile.
 The Edit methods provide a form for you to edit your profile, and to add the updated information into the database.
 The Delete methods provide a web page for you to delete your profile, and to delete you profile from the database.      
*/
namespace MovieViewerProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileRepository profileRepo;
        private readonly IUserRepository userRepo;

        public ProfileController(IProfileRepository profileRepository, IUserRepository userRepository)
        {
            profileRepo = profileRepository;
            userRepo = userRepository;

        }


        [HttpGet]
        public IActionResult Create()
        {
            var user = userRepo.Details(User.Identity.Name);
           
            ViewData["User"] = user;

            if(!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Identity/account/login");
            }
            else if(User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (user.Profile != null)
            {
                return RedirectToAction("Index", "Home");
            }


            return View();            
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string userName, [FromForm]ProfileVm profileVm)
        { 
            var user = userRepo.Details(User.Identity.Name);
            ViewData["User"] = user;

          

            var profile = new Profile
            {
                ProfileId = 0,
                ApplicationUserId = user.Id,
                User = user,
                CreditCardNumber = profileVm.CreditCardNumber,
                CreditCardExp = profileVm.CreditCardExp,
                StreetAddress = profileVm.StreetAddress,
                City = profileVm.City,
                State = profileVm.State,
                Zip = profileVm.Zip,
                MoneySpent = profileVm.MoneySpent,
                HasPaid = profileVm.HasPaid,
                WatchedCollection = profileVm.WatchedCollection

            };


            if (profile.CreditCardExp < DateTime.Now) ///chechk this
            {
                ModelState.AddModelError("CreditCardExp", "The credit card is expired");
            }

            if (ModelState.IsValid)
            {
                userRepo.CreateProfile(user.UserName, profile);
                await userRepo.AssignRoleAsync(user.UserName, "Movie Connessuir");
                return RedirectToAction("Index", "Home");                    
            }

            
            return View(profileVm);
        }


        public IActionResult Details(int id)
        {
            var user = userRepo.Details(User.Identity.Name);
            var profile = profileRepo.Details(id);
            ViewData["User"] = user;


            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Identity/account/login");
            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (user.Profile == null) 
            {
                return RedirectToAction("Create", "Profile");
            }

            ViewData["Profile"] = profile;
            return View(profile);
        }

        public IActionResult Edit(int id)
        {
            var user = userRepo.Details(User.Identity.Name);
            ViewData["User"] = user;

            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Identity/account/login");
            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (user.Profile == null)
            {
                return RedirectToAction("Create", "Profile");
            }



            var profile = profileRepo.Details(id);        
            if (profile == null)                                  
            {
                return RedirectToAction("Index","Movie");                   
            }

            ViewData["Profile"] = profile;
            return View(profile);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm] Profile profile)
        {
            if (ModelState.IsValid)
            {
                profileRepo.Edit(profile.ProfileId, profile);
                return RedirectToAction("Index", "Home");                  
            }

            return View(profile);
        }

        public IActionResult Delete(int id)
        {
            var user = userRepo.Details(User.Identity.Name);
            var profile = profileRepo.Details(id);
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Identity/account/login");
            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (user.Profile == null) 
            {
                return RedirectToAction("Create", "Profile");
            }

            return View(profile);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            profileRepo.Delete(id);
            return RedirectToAction("Index", "Home");
        }

         



    }
}
