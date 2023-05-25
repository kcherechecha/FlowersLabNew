using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlowersLab.Models;

namespace FlowersLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BouquetsController : ControllerBase
    {
        private readonly FlowerShopContext _context;

        public BouquetsController(FlowerShopContext context)
        {
            _context = context;
        }

        // GET: api/Bouquets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bouquet>>> GetBouquets()
        {
          if (_context.Bouquets == null)
          {
              return NotFound();
          }
            return await _context.Bouquets.ToListAsync();
        }

        // GET: api/Bouquets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bouquet>> GetBouquet(int id)
        {
          if (_context.Bouquets == null)
          {
              return NotFound();
          }
            var bouquet = await _context.Bouquets.FindAsync(id);

            if (bouquet == null)
            {
                return NotFound();
            }

            return bouquet;
        }

        // PUT: api/Bouquets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBouquet(int id, Bouquet bouquet)
        {
            if (id != bouquet.BouquetId)
            {
                return BadRequest();
            }

            _context.Entry(bouquet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BouquetExists(id))
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

        // POST: api/Bouquets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bouquet>> PostBouquet(Bouquet bouquet)
        {
          if (_context.Bouquets == null)
          {
              return Problem("Entity set 'FlowerShopContext.Bouquets'  is null.");
          }
            _context.Bouquets.Add(bouquet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBouquet", new { id = bouquet.BouquetId }, bouquet);
        }

        // DELETE: api/Bouquets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBouquet(int id)
        {
            if (_context.Bouquets == null)
            {
                return NotFound();
            }
            var bouquet = await _context.Bouquets.FindAsync(id);
            if (bouquet == null)
            {
                return NotFound();
            }

            _context.Bouquets.Remove(bouquet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BouquetExists(int id)
        {
            return (_context.Bouquets?.Any(e => e.BouquetId == id)).GetValueOrDefault();
        }
    }
}
