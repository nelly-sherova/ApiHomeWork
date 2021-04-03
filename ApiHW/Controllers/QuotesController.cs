using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiHW.Db;
using Microsoft.EntityFrameworkCore;
using ApiHW.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiHW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        QuotesContext data { get; set; }
        public QuotesController(QuotesContext context)
        {
            data = context;
        }
        [HttpPost]
        public async Task<ActionResult<Quotes>> Create(Quotes newQuotes) //Create
        {
            if (newQuotes == null)
            {
                //ModelState.AddModelError("quoteError", "Добаляемые данные некоррентны!");
                return BadRequest();
            }
            data.Quotes.Add(newQuotes);
            await data.SaveChangesAsync();
            return Ok("Данные добавлены!");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quotes>>> Read() // Select * 
        {
            return await data.Quotes.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Quotes>> Read(int id) //Select by id
        {
            Quotes quotes = await data.Quotes.FirstOrDefaultAsync(x => x.Id == id);
            if (quotes == null)
                return NotFound();
            return new ObjectResult(quotes);
        }
        [HttpPut]
        public async Task<ActionResult<Quotes>> Update(Quotes quotes)
        {
            if (quotes == null)
            {
                ModelState.AddModelError("quoteError", "Именяемые данные некоррентны!");
                return BadRequest();
            }
            if (!data.Quotes.Any(x => x.Id == quotes.Id))
            {
                ModelState.AddModelError("quoteError", "Именяемые данные не найдены!");
                return NotFound();
            }
            data.Update(quotes);
            await data.SaveChangesAsync();
            return Ok("Данные изменены!");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Quotes>> Delete(int id) //Delete
        {
            Quotes quotes = await data.Quotes.FirstOrDefaultAsync(x => x.Id == id);
            if (quotes == null)
            {
                ModelState.AddModelError("quoteError", "Удаляемые данные не найдены!");
                return NotFound();
            }
            data.Quotes.Remove(quotes);
            await data.SaveChangesAsync();
            return Ok("Данные удалены");
        }

        public void AutoDeteling(Quotes quotes)
        {
            DateTime quoteDate = quotes.InsertDate;
            DateTime limitDate = DateTime.Now.AddMonths(1);
            if (DateTime.Compare(quoteDate, limitDate) > 0)
            {
                data.Quotes.Remove(quotes);
                data.SaveChanges();
            }
        }
    }
}
