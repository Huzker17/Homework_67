using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CountryController(ApplicationDbContext context)
        {
            _db = context;
            if (!_db.Countries.Any())
            {
                _db.Countries.Add(new Country { Name = "Tom" });
                _db.Countries.Add(new Country { Name = "Alice" });
                _db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> Get()
        {
            return await _db.Countries.ToListAsync();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> Get(int id)
        {
            Country country = await _db.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if (country == null)
                return NotFound();
            return new ObjectResult(country);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<Country>> Post(Country country)
        {
            if (country == null)
            {
                return BadRequest();
            }
            _db.Countries.Add(country);
            await _db.SaveChangesAsync();
            return Ok(country);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<Country>> Put(Country country)
        {
            if (country == null)
            {
                return BadRequest();
            }
            if (!_db.Countries.Any(x => x.Id == country.Id))
            {
                return NotFound();
            }
            _db.Update(country);
            await _db.SaveChangesAsync();
            return Ok(country);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Country>> Delete(int id)
        {
            Country country = _db.Countries.FirstOrDefault(x => x.Id == id);
            if (country == null)
            {
                return NotFound();
            }
            _db.Countries.Remove(country);
            await _db.SaveChangesAsync();
            return Ok(country);
        }
    }
}
