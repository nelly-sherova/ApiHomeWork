using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiHW.Db;
using Microsoft.EntityFrameworkCore;
using ApiHW.Models;



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
        [Route("Create")]
        public async Task<ActionResult<Quotes>> Create([FromBody]Quotes newQuotes)
        { 
            if (newQuotes == null)
            {
                ModelState.AddModelError("quoteError", "Добаляемые данные некоррентны!");
                return BadRequest(ModelState);
            }
            data.Quotes.Add(newQuotes);
            await data.SaveChangesAsync();
            return Ok("Данные добавлены!");
        }
        [HttpGet]
		[Route("Get")]
        public async Task<ActionResult<IEnumerable<Quotes>>> Read()
        {
            return await data.Quotes.ToListAsync();
        }
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<ActionResult<Quotes>> Read([FromRoute]int id) 
        {
            Quotes quotes = await data.Quotes.FirstOrDefaultAsync(x => x.Id == id);
            if (quotes == null)
                return NotFound(ModelState);
            return new ObjectResult(quotes);
        }
        [HttpPost]
        [Route("Edit/{id}")]
        public async Task<ActionResult<Quotes>> Update([FromBody]Quotes quotes, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("quoteError", "Именяемые данные некоррентны!");
                return BadRequest(ModelState);
            }
            var quote = await data.Quotes.FindAsync(id);
            if(quote==null)
            {
                ModelState.AddModelError("quoteError", "Именяемые данные не найдены!");
                return NotFound(ModelState);
            }
            quote.Text = quotes.Text;
            quote.Author = quotes.Author;
            quote.InsertDate = quotes.InsertDate;
            await data.SaveChangesAsync();
            return Ok("Данные изменены!");
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult<Quotes>> Delete([FromRoute]int id) 
        {
            Quotes quotes = await data.Quotes.FirstOrDefaultAsync(x => x.Id == id);
            if (quotes == null)
            {
                ModelState.AddModelError("quoteError", "Удаляемые данные не найдены!");
                return NotFound(ModelState);
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
