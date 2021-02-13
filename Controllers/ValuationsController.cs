using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BrighterCapAPI.Models;
using BrighterCapAPI.Helpers;

namespace BrighterCapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuationsController : ControllerBase
    {
        private readonly DBContext _context;

        public ValuationsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Valuations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Valuation>>> GetValuation()
        {
            return await _context.Valuation.ToListAsync();
        }

        // GET: api/Valuations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Valuation>> GetValuation(string id)
        {
            var valuation = await _context.Valuation.FindAsync(id);

            if (valuation == null)
            {
                return NotFound();
            }

            return valuation;
        }

        // POST: api/Valuations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostValuation(Valuation valuation)
        {
            _context.Valuation.Add(valuation);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ValuationExists(valuation.ParcelId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(201);
        }

        [HttpPost]
        [Route("PostArray")]
        public async Task<ActionResult> PostValuationData(Valuation[] valuation)
        {
            await _context.Valuation.AddOrUpdateRange(valuation, _context, (item1, item2) =>
                 item1.ParcelId == item2.ParcelId &&
                 item1.ValuationDate == item2.ValuationDate);

            return StatusCode(201);
        }

        private bool ValuationExists(string id)
        {
            return _context.Valuation.Any(e => e.ParcelId == id);
        }
    }
}
