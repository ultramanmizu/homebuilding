using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeBuilding;
using System.Threading.Tasks;

namespace HomeBuilding.Controllers
{
    public class UserController : Controller
    {
        private HomeBuildingEntities db = new HomeBuildingEntities();

        // GET: User
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password,Title,FirstName,LastName,FullName,RoleId,RoleName,Email,LastLogIn,LastChangePassword,Description,Sequence,IsEnabled,IsDeleted,CreatedById,CreatedDate,UpdatedById,UpdatedDate,DeletedById,DeletedDate")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,Title,FirstName,LastName,FullName,RoleId,RoleName,Email,LastLogIn,LastChangePassword,Description,Sequence,IsEnabled,IsDeleted,CreatedById,CreatedDate,UpdatedById,UpdatedDate,DeletedById,DeletedDate")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //GET: Login
        [HttpGet]
        public async Task<ActionResult> Login(string username, string password) {
            if (username == null || password == null) { TempData["shortMessage"] = "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง!";  return RedirectToAction("signin", "Home"); }
            //get user in database
            User user = db.Users.Where(x => x.Username == username && x.Password == password && x.IsEnabled == true && x.IsDeleted == false).FirstOrDefault();
            if(user == null) { TempData["shortMessage"] = "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง!"; return RedirectToAction("signin", "Home"); }
            //update last login
            user.LastLogIn = DateTime.Now;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            //save user profile in to session
            Session["UserProfile.Id"] = user.Id;
            Session["UserProfile.Fullname"] = user.FullName;
            Session["UserProfile.Role"] = user.RoleName;

            //set first page after login by role
            switch (user.RoleName) {
                case "User": return RedirectToAction("search", "Receipt");
                case "Admin": return RedirectToAction("search", "Receipt");
                case "Super User": return RedirectToAction("search", "Receipt"); 
                default: return RedirectToAction("search", "Receipt");
            }
        }

        public ActionResult Logout()
        {
            //clear session
            Session.Clear();
            return RedirectToAction("signin", "Home");
        }
    }
}
