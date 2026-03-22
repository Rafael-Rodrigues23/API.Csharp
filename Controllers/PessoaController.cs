using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;


namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {


        private DataContext dc;

        public PessoaController(DataContext context)
        {
            this.dc = context;
        }

        [HttpPost("api")]
        public async Task<ActionResult> cadastrar([FromBody] Pessoa p)
        {
            dc.Pessoas.Add(p);
            await dc.SaveChangesAsync();

            return Created("Objeto Pessoa", p);
        }

        [HttpGet("api")]
        public async Task<ActionResult> listar()
        {
            var dados = await dc.Pessoas.ToListAsync();
            return Ok(dados);
        }

        [HttpGet("api/{codigo}")]
        public async Task<ActionResult<Pessoa>> filtrar(int codigo)
        {
            var p = await dc.Pessoas.FindAsync(codigo);
            if (p == null)
            {
                return NotFound();
            }
            return Ok(p);
        }

        [HttpDelete("api/{codigo}")]
        public async Task<ActionResult> remover(int codigo)
        {
            var p = await dc.Pessoas.FindAsync(codigo);
            if (p == null)
            {
                return NotFound();
            }

            dc.Pessoas.Remove(p);
            await dc.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("api")]
        public async Task<ActionResult> editar([FromBody] Pessoa p)
        {
            dc.Pessoas.Update(p);
            await dc.SaveChangesAsync();
            return Ok(p);
        }

        [HttpGet("oi")]
        public string oi()
        {
            return ("Hello Word");
        }
    }
}