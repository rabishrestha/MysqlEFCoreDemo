using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MysqlEFCoreDemo.Data;
using MysqlEFCoreDemo.Models;
using System.Diagnostics;

namespace MysqlEFCoreDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;

        public HomeController(ILogger<HomeController> 
            logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public IActionResult Index()
        {
            var people = _context.Person.ToList();
            return View(people);
        }

       
        // GET: PersonInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var PersonInfo = await _context.Person
                .FirstOrDefaultAsync(m => m.id == id);
            if (PersonInfo == null)
            {
                return NotFound();
            }

            return View(PersonInfo);
        }

        // POST: PersonInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'MyDbContext.phonebook'  is null.");
            }
            var PersonInfo = await _context.Person.FindAsync(id);
            if (PersonInfo != null)
            {
                _context.Person.Remove(PersonInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: PersonInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var phonebookInfo = await _context.Person.FindAsync(id);
            if (phonebookInfo == null)
            {
                return NotFound();
            }
            return View(phonebookInfo);
        }

        // POST: PersonInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,first_name,last_name,is_player")] PersonModel PersonInfo)
        {
            if (id != PersonInfo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(PersonInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(PersonInfo.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(PersonInfo);
        }

        private bool PersonExists(int id)
        {
            return (_context.Person?.Any(e => e.id == id)).GetValueOrDefault();
        }


        // GET: PersonInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,first_name,last_name,is_player")] PersonModel PersonInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(PersonInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(PersonInfo);
        }

        // GET: PersonInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Person == null)
            { 
                return NotFound();
            }

            var PersonInfo = await _context.Person
                .FirstOrDefaultAsync(m => m.id == id);
            if (PersonInfo == null)
            {
                return NotFound();
            }

            return View(PersonInfo);
        }
    }
}