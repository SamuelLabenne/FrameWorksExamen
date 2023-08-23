using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FrameWorksExamen.Data;
using FrameWorksExamen.Models;
using Microsoft.AspNetCore.Authorization;

namespace FrameWorksExamen.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class InvitesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InvitesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Invites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invite>>> GetInvite()
        {
          if (_context.Invite == null)
          {
              return NotFound();
          }
            return await _context.Invite.ToListAsync();
        }

        // GET: api/Invites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invite>> GetInvite(int id)
        {
          if (_context.Invite == null)
          {
              return NotFound();
          }
            var invite = await _context.Invite.FindAsync(id);

            if (invite == null)
            {
                return NotFound();
            }

            return invite;
        }

        // PUT: api/Invites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvite(int id, Invite invite)
        {
            if (id != invite.Id)
            {
                return BadRequest();
            }

            _context.Entry(invite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InviteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Invites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invite>> PostInvite(Invite invite)
        {
          if (_context.Invite == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Invite'  is null.");
          }
            _context.Invite.Add(invite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvite", new { id = invite.Id }, invite);
        }

        // DELETE: api/Invites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvite(int id)
        {
            if (_context.Invite == null)
            {
                return NotFound();
            }
            var invite = await _context.Invite.FindAsync(id);
            if (invite == null)
            {
                return NotFound();
            }

            invite.deleted=true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InviteExists(int id)
        {
            return (_context.Invite?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
