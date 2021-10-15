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
            if (!_db.CountriesContext.Any())
            {
                _db.CountriesContext.Add(new Country {Id = 1, Name = "Austria",Capital ="Vena",Currency= "EUR" });
                _db.CountriesContext.Add(new Country { Id = 2, Name = "Germany", Capital = "Munich", Currency = "EUR" } );
                _db.CountriesContext.Add(new Country { Id = 2, Name = "Spain", Capital = "Madrid", Currency = "EUR" });
                _db.CountriesContext.Add(new Country { Id = 2, Name = "Italy", Capital = "Rome", Currency = "EUR" });
                _db.CountriesContext.Add(new Country { Id = 2, Name = "Island", Capital = "Reykjavik", Currency = "ISK" });
                _db.CountriesContext.Add(new Country { Id = 2, Name = "Hungary", Capital = "Budapest", Currency = "HUF" });
                _db.CountriesContext.Add(new Country { Id = 2, Name = "England", Capital = "London", Currency = "GBP" });
                _db.CountriesContext.Add(new Country { Id = 2, Name = "Belarus", Capital = "Minsk", Currency = "BYR" });
                _db.CountriesContext.Add(new Country { Id = 2, Name = "Sweden", Capital = "Stickholm", Currency = "SEK" });
                _db.CountriesContext.Add(new Country { Id = 2, Name = "Serbia", Capital = "Belgrade", Currency = "RSD" });
                _db.CountriesContext.Add(new Country { Id = 2, Name = "Romania", Capital = "Bucharest", Currency = "RON" });
                _db.CountriesContext.Add(new Country { Id = 2, Name = "Norway", Capital = "Oslo", Currency = "NOK" });
                _db.CountriesContext.Add(new Country { Id = 2, Name = "Moldova", Capital = "Chisinau", Currency = "MDL" });
                _db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> Get()
        {
            return await _db.CountriesContext.ToListAsync();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> Get(int id)
        {
            Country country = await _db.CountriesContext.FirstOrDefaultAsync(x => x.Id == id);
            if (country == null)
                return NotFound();
            return new ObjectResult(country);
        }
        [HttpGet("{name}")]
        public async Task<ActionResult<Country>> Get(string name)
        {
            Country country = await _db.CountriesContext.FirstOrDefaultAsync(x => x.Name == name);
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
            _db.CountriesContext.Add(country);
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
            if (!_db.CountriesContext.Any(x => x.Id == country.Id))
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
            Country country = _db.CountriesContext.FirstOrDefault(x => x.Id == id);
            if (country == null)
            {
                return NotFound();
            }
            _db.CountriesContext.Remove(country);
            await _db.SaveChangesAsync();
            return Ok(country);
        }
    }
}
