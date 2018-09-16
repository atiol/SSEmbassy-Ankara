using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ssembassy_ankara.Models;

namespace ssembassy_ankara.Controllers
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Roles
        public ActionResult Index()
        {
            // prepopulate roles for the view dropdown
            try
            {
                var list = _db.Roles.OrderBy(r => r.Name).ToList().Select(rr =>
                    new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();

                ViewBag.Roles = list.Count > 0 ? list : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ViewBag.Roles = null;
            }

            return View();
        }

        // POST: Add Roles
        [HttpPost]
        public ActionResult AddRole(FormCollection collection)
        {
            try
            {
                _db.Roles.Add(new IdentityRole()
                {
                    Name = collection["Role1"]
                }
                );
                _db.SaveChanges();
                ViewBag.AddRoleMsg = "Role added successfully";

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ViewBag.AddRoleMsg = e.Message;
                return View("Index");
            }
        }

        // POST: Assign roles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignRoles(string UserName, string RoleName)
        {
            try
            {
                var User = _db.Users.FirstOrDefault(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase));

                // prepopulat roles for the view dropdown
                var list = _db.Roles.OrderBy(r => r.Name).ToList()
                    .Select(rr => new SelectListItem {Value = rr.Name.ToString(), Text = rr.Name}).ToList();

                if (User == null)
                {
                    ViewBag.RoleAssignMsg = "The user provided doesn't exist!";
                    return View("Index");
                }

                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));

                if (!manager.IsInRole(User.Id, RoleName))
                {
                    manager.AddToRole(User.Id, RoleName);
                }
                else
                {
                    ViewBag.RoleAssignMsg = "Role already assigned to user.";
                    ViewBag.Roles = list;

                    return View("Index");
                }

                ViewBag.RoleAssignMsg = "Role assigned successfully.";
                ViewBag.Roles = list;
            }
            catch(Exception e)
            {
                ViewBag.Roles = null;
                ViewBag.RoleAssignMsg = e.Message;
            }

            return View("Index");
        }

        // GET userRoles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetUserRoles(string UserName2)
        {
            // prepopulate roles for the view dropdown
            var list = _db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            if (!string.IsNullOrWhiteSpace(UserName2))
            {               
                ApplicationUser user = _db.Users.FirstOrDefault(u => u.UserName.Equals(UserName2, StringComparison.CurrentCultureIgnoreCase));
                if (user == null)
                {
                    ViewBag.UserRolesError = "User does not exist!";
                    ViewBag.Roles = list;

                    return View("Index");
                }

                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
                ViewBag.UserRoles = manager.GetRoles(user.Id);
            }
            else
            {
                ViewBag.UserRolesError = "Error occured! Are you sure of the UserName?";
            }

            ViewBag.Roles = list;
            return View("Index");
        }

        // Delete Role
        public ActionResult Delete(string RoleName)
        {
            var thisRole = _db.Roles.FirstOrDefault(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase));
            _db.Roles.Remove(thisRole);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // Delete Role from User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleFromUser(string User, string Role)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));

            ApplicationUser user = _db.Users.FirstOrDefault(u => u.UserName.Equals(User, StringComparison.CurrentCultureIgnoreCase));

            if ( user != null && manager.IsInRole(user.Id, Role))
            {
                manager.RemoveFromRole(user.Id, Role);
                ViewBag.RoleDeletedMsg = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.RoleDeletedMsg = "This user doesn't belong to selected role.";
            }
            // prepopulate roles for the view dropdown
            var list = _db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("Index");
        }
    }
}