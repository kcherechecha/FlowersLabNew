using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlowersLab.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using static NuGet.Packaging.PackagingConstants;

namespace FlowersLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowersController : ControllerBase
    {
        private readonly FlowerShopContext _context;

        public FlowersController(FlowerShopContext context)
        {
            _context = context;
        }

        // GET: api/Flowers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flower>>> GetFlowers()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            var flower = await _context.Flowers.Include(b => b.Bouquets).ToArrayAsync();

          if (_context.Flowers == null)
          {
                return NotFound();
          }

            var json = JsonSerializer.Serialize(flower, options);

            return Content(json, "application/json");
            //return flower;//await _context.Flowers.ToListAsync();
        }

        // GET: api/Flowers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flower>> GetFlower(int id)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            if (_context.Flowers == null)
          {
              return NotFound();
          }
            var flower = await _context.Flowers.Include(b => b.Bouquets).FirstOrDefaultAsync(b => b.FlowerId == id);

            if (flower == null)
            {
                return NotFound();
            }

            var json = JsonSerializer.Serialize(flower, options);

            return Content(json, "application/json");
            //return flower;
        }

        // PUT: api/Flowers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlower(int id, Flower flower)
        {
            if (id != flower.FlowerId)
            {
                return BadRequest();
            }

            bool flowerExists = await _context.Flowers.AnyAsync(f => f.FlowerName == flower.FlowerName);
            if (flowerExists)
            {
                ModelState.AddModelError("FlowerName", "A flower with the same name already exists.");
                return BadRequest(ModelState);
            }

            _context.Entry(flower).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlowerExists(id))
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

        // POST: api/Flowers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Flower>> PostFlower(Flower flower)
        {
          if (_context.Flowers == null)
          {
              return Problem("Entity set 'FlowerShopContext.Flowers'  is null.");
          }

          bool flowerExists = await _context.Flowers.AnyAsync(f => f.FlowerName == flower.FlowerName);
          if (flowerExists)
          {
              ModelState.AddModelError("FlowerName", "A flower with the same name already exists.");
              return BadRequest(ModelState);
          }

            _context.Flowers.Add(flower);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlower", new { id = flower.FlowerId }, flower);
        }

        // DELETE: api/Flowers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlower(int id)
        {
            if (_context.Flowers == null)
            {
                return NotFound();
            }
            var flower = await _context.Flowers.FindAsync(id);
            if (flower == null)
            {
                return NotFound();
            }

            _context.Flowers.Remove(flower);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FlowerExists(int id)
        {
            return (_context.Flowers?.Any(e => e.FlowerId == id)).GetValueOrDefault();
        }
    }
}
