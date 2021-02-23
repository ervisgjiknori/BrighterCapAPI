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
    public class CountyDatasController : ControllerBase
    {
        private readonly DBContext _context;

        public CountyDatasController(DBContext context)
        {
            _context = context;
        }

        // GET: api/CountyDatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountyData>>> GetCountyData()
        {
            return await _context.CountyData.ToListAsync();
        }

        // GET: api/CountyDatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountyData>> GetCountyData(string id)
        {
            var countyData = await _context.CountyData.FindAsync(id);

            if (countyData == null)
            {
                return NotFound();
            }

            return countyData;
        }

        // POST: api/CountyDatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostCountyData(CountyData countyData)
        {
            _context.CountyData.Add(countyData);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CountyDataExists(countyData.ParcelId))
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
        public async Task<ActionResult> PostCountyDataData(CountyData[] countyData)
        {
            var data = countyData.GroupBy(row => row.ParcelId).Select(group => group.FirstOrDefault()).ToList();
            await _context.CountyData.AddOrUpdateRange(data, _context, (item1, item2) =>
                item1.ParcelId == item2.ParcelId);
            return StatusCode(201);
        }

        private bool CountyDataExists(string id)
        {
            return _context.CountyData.Any(e => e.ParcelId == id);
        }
    }
}
