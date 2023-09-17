using Microsoft.AspNetCore.Mvc;
using Pustok.Database;

namespace ApplicationDbContext
{
    public class ApplicationDbContext : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PustokDbContext _pustokDbContext;

        public ApplicationDbContext(ApplicationDbContext context, PustokDbContext pustokDbContext)
        {
            _context = context;
            _pustokDbContext = pustokDbContext;
        }

        public IActionResult ActivateAccount(string code)
        {
            var user = _pustokDbContext.Users.SingleOrDefault(u => u.ActivationCode == code);

            if (user == null)
            {
                TempData["Message"] = "Time out.";
                return RedirectToAction("Index", "Home");
            }

            if (user.ActivationCodeExpiration >= DateTime.UtcNow)
            {
                user.ActivationCode = null;

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
