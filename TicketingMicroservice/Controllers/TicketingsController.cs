using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketingMicroservice.Database;

namespace TicketingMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketingsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TicketingsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Ticketings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticketing>>> GetTicketings()
        {
            return await _context.Ticketings.ToListAsync();
        }

        // GET: api/Ticketings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticketing>> GetTicketing(int id)
        {
            var ticketing = await _context.Ticketings.FindAsync(id);

            if (ticketing == null)
            {
                return NotFound();
            }

            return ticketing;
        }

        // PUT: api/Ticketings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketing(int id, Ticketing ticketing)
        {
            if (id != ticketing.TicketId)
            {
                return BadRequest();
            }

            _context.Entry(ticketing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketingExists(id))
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

        // POST: api/Ticketings
        [HttpPost]
        public async Task<ActionResult<Ticketing>> PostTicketing(Ticketing ticketing)
        {
            _context.Ticketings.Add(ticketing);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicketing", new { id = ticketing.TicketId }, ticketing);
        }

        // DELETE: api/Ticketings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ticketing>> DeleteTicketing(int id)
        {
            var ticketing = await _context.Ticketings.FindAsync(id);
            if (ticketing == null)
            {
                return NotFound();
            }

            _context.Ticketings.Remove(ticketing);
            await _context.SaveChangesAsync();

            return ticketing;
        }

        private bool TicketingExists(int id)
        {
            return _context.Ticketings.Any(e => e.TicketId == id);
        }
    }
}
