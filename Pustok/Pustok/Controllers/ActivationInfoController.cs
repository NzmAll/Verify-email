using Microsoft.AspNetCore.Mvc;

namespace ApplicationDbContext
{
    public class ApplicationDbContext : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbContext(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult ActivateAccount(string code)
        {
            var user = _context.User.SingleOrDefault(u => u.ActivationCode == code);

            if (user == null)
            {
                TempData["Message"] = "Time out.";
                return RedirectToAction("Index", "Home");
            }

            if (user.ActivationCodeExpiration >= DateTime.UtcNow)
            {
                user.IsActive = true;
                user.ActivationCode = null;
                user.ActivationCodeExpiration = null;

                _context.SaveChanges();
                TempData["Message"] = "Account active. U are login now.";
                return RedirectToAction("Login", "Auth");
            }

            TempData["Message"] = "Time out.";
            return RedirectToAction("Index", "Home");
        }

        private void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
