using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiWebAPI.Entidades;

namespace MiWebAPI.Controllers
{
    [Route("api/zapatillas")]
    public class ZapatillasController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        public ZapatillasController(ApplicationDbContext context) 
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<List<Zapatillas>> Get()
        {
            return await context.Zapatillas.ToListAsync();
        }
        [HttpGet("{id:int}", Name ="ObtenerZapatillaPorId")]
        public async Task<ActionResult<Zapatillas>> Get(int id)
        {
            var zapatillas = await context.Zapatillas.FirstOrDefaultAsync(x => x.Id == id);
            if(zapatillas is null)
            {
                return NotFound();
            }
            return zapatillas;
        }
        [HttpPost]
        public async Task<CreatedAtRouteResult> Post ([FromBody] Zapatillas zapatillas)
        {
            context.Add(zapatillas);
            await context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerZapatillaPorId", new {id = zapatillas.Id}, zapatillas);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id,[FromBody] Zapatillas zapatillas)
        {
            var existeZapatilla = await context.Zapatillas.AnyAsync(x => x.Id == id);
            if (!existeZapatilla)
            {
                return NotFound();
            }
            zapatillas.Id=id;

            context.Update(zapatillas);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var filasBorradas =await context.Zapatillas.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (filasBorradas == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
