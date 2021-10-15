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
                _db.CountriesContext.Add(new Country { Id = 3, Name = "Spain", Capital = "Madrid", Currency = "EUR" });
                _db.CountriesContext.Add(new Country { Id = 4, Name = "Italy", Capital = "Rome", Currency = "EUR" });
                _db.CountriesContext.Add(new Country { Id = 5, Name = "Island", Capital = "Reykjavik", Currency = "ISK" });
                _db.CountriesContext.Add(new Country { Id = 6, Name = "Hungary", Capital = "Budapest", Currency = "HUF" });
                _db.CountriesContext.Add(new Country { Id = 7, Name = "England", Capital = "London", Currency = "GBP" });
                _db.CountriesContext.Add(new Country { Id = 8, Name = "Belarus", Capital = "Minsk", Currency = "BYR" });
                _db.CountriesContext.Add(new Country { Id = 9, Name = "Sweden", Capital = "Stickholm", Currency = "SEK" });
                _db.CountriesContext.Add(new Country { Id = 10, Name = "Serbia", Capital = "Belgrade", Currency = "RSD" });
                _db.CountriesContext.Add(new Country { Id = 11, Name = "Romania", Capital = "Bucharest", Currency = "RON" });
                _db.CountriesContext.Add(new Country { Id = 12, Name = "Norway", Capital = "Oslo", Currency = "NOK" });
                _db.CountriesContext.Add(new Country { Id = 13, Name = "Moldova", Capital = "Chisinau", Currency = "MDL" });
                _db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> Get()
        {
            return await _db.CountriesContext.ToListAsync();
        }

        // GET api/country/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> Get(int id)
        {
            Country country = await _db.CountriesContext.FirstOrDefaultAsync(x => x.Id == id);
            if (country == null)
                return NotFound();
            return new ObjectResult(country);
        }
        [HttpGet("getbyname/{name}")]
        public async Task<ActionResult<Country>> GetByName(string name)
        {
            Country country = await _db.CountriesContext.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
            if (country == null)
                return NotFound();
            return new ObjectResult(country);
        }

        // POST api/country
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

        // PUT api/country/
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

        // DELETE api/country/5
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
