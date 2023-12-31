﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrameWorksExamen.Data;
using FrameWorksExamen.Models;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;

namespace FrameWorksExamen.Controllers
{
    public class InvitesController : ApplicationController
    {

        private readonly IStringLocalizer<PeopleController> _localizer;
        public InvitesController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<ApplicationController> logger, IStringLocalizer<PeopleController> localizer)
            : base(context, httpContextAccessor, logger)
        {
         _localizer = localizer;

        }

        // GET: Invites
        public async Task<IActionResult> Index(int selectedEvent, int selectedPerson)
        {
           
            var filteredInvites = from i in _context.Invite
                                  where (i.deleted.Equals(false))
                                  select i;
            if(selectedEvent != 0)
            {
                filteredInvites = from i in filteredInvites where i.EventId == selectedEvent select i;

            }
            if (selectedPerson != 0)
            {
                filteredInvites = from i in filteredInvites where i.PersonId == selectedPerson select i;

            }
            IQueryable<Event> eventsToSelect = from e in _context.Event orderby e.Name select e;
            IQueryable<Person> peopleToSelect = from p in _context.Person orderby p.Name select p;

            InviteIndexViewModel inviteIndexViewModel = new InviteIndexViewModel()
            {
                FilteredInvites = await filteredInvites.Include(i => i.Event).Include(i => i.Person).Include(i=>i.Sender).ToListAsync(),
                SelectedEvent = selectedEvent,
                SelectedPerson = selectedPerson,
                PeopleToSelect = new SelectList(await peopleToSelect.ToListAsync(), "Id", "Name", selectedPerson),
                EventsToSelect = new SelectList(await eventsToSelect.ToListAsync(), "Id","Name", selectedEvent)
            };
            return View(inviteIndexViewModel);
        }

        // GET: Invites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Invite == null)
            {
                return NotFound();
            }

            var invite = await _context.Invite
                .Include(i => i.Event)
                .Include(i => i.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invite == null)
            {
                return NotFound();
            }

            return View(invite);
        }
        [Authorize]
        // GET: Invites/Create
        public IActionResult Create()
        {
            Invite invite = new Invite();
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name");
            ViewData["PersonId"] = new SelectList(_context.Person, "Id", "Name");
            return View(invite);
        }

        // POST: Invites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonId,EventId,deleted,Sender,SenderId")] Invite invite)
        {
            invite.Sender =_user;
            _context.Add(invite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }
        [Authorize]
        // GET: Invites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Invite == null)
            {
                return NotFound();
            }

            var invite = await _context.Invite.FindAsync(id);
            
            if (invite == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", invite.EventId);
            ViewData["PersonId"] = new SelectList(_context.Person, "Id", "Name", invite.PersonId);
            return View(invite);
        }

        // POST: Invites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonId,EventId,deleted,Sender,SenderId")] Invite invite)
        {
            
            if (id != invite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InviteExists(invite.Id))
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
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Name", invite.EventId);
            ViewData["PersonId"] = new SelectList(_context.Person, "Id", "Name", invite.PersonId);
            return View(invite);
        }
        [Authorize]
        // GET: Invites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Invite == null)
            {
                return NotFound();
            }

            var invite = await _context.Invite
                .Include(i => i.Event)
                .Include(i => i.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invite == null)
            {
                return NotFound();
            }

            return View(invite);
        }

        // POST: Invites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            if (_context.Invite == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Invite'  is null.");
            }
            var invite = await _context.Invite.FindAsync(id);
            if (invite != null)
            {
                invite.deleted=true;
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InviteExists(int id)
        {
          return (_context.Invite?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
