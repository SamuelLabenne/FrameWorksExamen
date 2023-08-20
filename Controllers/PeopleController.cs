using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrameWorksExamen.Data;
using FrameWorksExamen.Models;

namespace FrameWorksExamen.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var people = from p in _context.Person.Include(p => p.Events)
                         where (p.deleted.Equals(false))
                         orderby p.Name
                         select p;
            return _context.Person != null ? 
                          View(await people.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Person'  is null.");
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            Person p = new Person();
            p.EventsId = new List<int>();
            if (p.Events != null)
            {
                foreach (Event e in p.Events)
                {
                    p.EventsId.Add(e.Id);
                }
            }
            ViewData["EventsId"] = new MultiSelectList(_context.Event.OrderBy(e => e.Name), "Id", "Name", p.EventsId);


            return View(p);
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age,deleted,EventsId,Events")] Person person)
        {
            if (ModelState.IsValid)
            {
                if (person.Events == null)
                {
                    person.Events = new List<Event>();
                    foreach (int id in person.EventsId)
                    {
                        person.Events.Add(_context.Event.FirstOrDefault(e => e.Id == id));
                    }
                }
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

                ViewData["EventsId"] = new MultiSelectList(_context.Event.OrderBy(e => e.Name), "Id", "Name", person.EventsId);
                return View(person);
            }
        

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["EventsId"] = new MultiSelectList(_context.Event.OrderBy(e => e.Name), "Id", "Name", person.EventsId);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,deleted,EventsId,Events")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (person.Events == null)
                    {
                        person.Events = new List<Event>();
                        foreach (int i in person.EventsId)
                        {
                            person.Events.Add(_context.Event.FirstOrDefault(e => e.Id == i));
                        }
                    }
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Person'  is null.");
            }
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                person.deleted = true;
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
          return (_context.Person?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
