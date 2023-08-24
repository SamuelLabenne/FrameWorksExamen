using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrameWorksExamen.Data;
using FrameWorksExamen.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace FrameWorksExamen.Controllers
{
    public class EventsController : ApplicationController
    {
        private readonly IStringLocalizer<EventsController> _localizer;

        public EventsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<ApplicationController> logger, IStringLocalizer<EventsController> localizer)
            : base(context, httpContextAccessor, logger)
        {
            _localizer = localizer;
        }

        // GET: Events
        public async Task<IActionResult> Index(string searchField, string searchField2)
        {
            var events = from e in _context.Event.Include(e=>e.Invited)
                         where (e.deleted.Equals(false))
                         orderby e.Name
                         select e;

            if (!string.IsNullOrEmpty(searchField))
            {
                events = from e in events
                         where e.Name.Contains(searchField)
                         orderby e.Name
                         select e;
            }
            if (!string.IsNullOrEmpty(searchField2))
            {
                events = from e in events
                         where e.Description.Contains(searchField2)
                         orderby e.Description
                         select e;
            }
            

            return _context.Event != null ?
                          View(await events.ToListAsync()) :
                          Problem("Entity set 'SL_Frameworks_FinalContext.Event'  is null.");

        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Event == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["Name"] = @event.Name;

            return View(@event);
        }
        [Authorize]
        // GET: Events/Create
        public IActionResult Create()
        {
            Event ev = new Event();
            ev.PeopleId = new List<int>();
            if(ev.Invited != null)
            {
                foreach(Person p in ev.Invited)
                {
                    ev.PeopleId.Add(p.Id);
                }
            }
            ViewData["PeopleId"] = new MultiSelectList(_context.Person.OrderBy(c => c.Name), "Id", "Name");
            
            
            return View(ev);
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Location,date,people,deleted,Invited,PeopleId")] Event @event)
        {
            if (ModelState.IsValid)
            {
                if(@event.Invited == null)
                {
                    @event.Invited = new List<Person>();
                    foreach(int id in @event.PeopleId)
                    {
                        @event.Invited.Add(_context.Person.FirstOrDefault(p => p.Id == id));
                    }
                }
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeopleId"] = new MultiSelectList(_context.Person.OrderBy(c => c.Name), "Id", "Name");
            return View(@event);
        }
        [Authorize]
        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Event == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["PeopleId"] = new MultiSelectList(_context.Person.OrderBy(c => c.Name), "Id", "Name");
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Location,date,people,deleted,Invited,PeopleId")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (@event.Invited == null)
                    {
                        @event.Invited = new List<Person>();
                        foreach (int i in @event.PeopleId)
                        {
                            @event.Invited.Add(_context.Person.FirstOrDefault(p => p.Id == i));
                        }
                    }
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            return View(@event);
        }
        [Authorize]
        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Event == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Event == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Event'  is null.");
            }
            var @event = await _context.Event.FindAsync(id);
            if (@event != null)
            {
                
                @event.deleted = true;
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
          return (_context.Event?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
