using Microsoft.AspNetCore.Mvc;
using MysqlEFCoreDemo.Data;

namespace MysqlEFCoreDemo.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;

        public ContactController(ILogger<HomeController>
            logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var contact = _context.Contact.ToList();
            return View(contact);
        }
    }
}
