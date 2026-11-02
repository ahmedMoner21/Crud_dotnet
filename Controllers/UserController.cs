using AZMStore.Data;
using AZMStore.Models;
using AZMStore.Repo;
using Microsoft.AspNetCore.Mvc;

namespace AZMStore.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepo _userRepo;

        public UserController(ApplicationDbContext context, IUserRepo userRepo)
        {
            _context = context;
            _userRepo = userRepo;
        }

        //get all users
        public IActionResult SelectAll()
        {
            var users = _userRepo.GetAllUsers();
            return View("Index",users);
        }
        //get user by id
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View("UserDetails", user);
        } 

        public IActionResult CreateForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {

            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("SelectAll");

        }

        public IActionResult UpdateForm(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);

        }
        [HttpPost]
        public IActionResult UpdateUser(User user)
        {
            var CheckUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (CheckUser == null)
            {
                return NotFound();
            }
            //_context.Users.Update(CheckUser);
            CheckUser.Name = user.Name;
            CheckUser.Email = user.Email;
            CheckUser.Phone = user.Phone;
            _context.SaveChanges();
            return RedirectToAction("SelectAll");
        }

        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("SelectAll");
        }


    }
}
