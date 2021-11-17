using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        public IEnumerable<Evento> _eventos = new Evento[] {
                new Evento() {
                    Id = 1,
                    Tema = "Angular 11 e .NET 5",
                    Local = "Belo Horizonte",
                    Lote = "1 Lote",
                    QuantidadePessoas = 250,
                    DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")
                },
                new Evento() {
                    Id = 2,
                    Tema = "Angular e suas novidades",
                    Local = "Sao Paulo",
                    Lote = "2 Lote",
                    QuantidadePessoas = 350,
                    DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")
                }
            };
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public EventosController() { }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _eventos;
        }

        [HttpGet("{id:int}")]
        public IEnumerable<Evento> GetById(int id)
        {
            return _eventos.Where(evento => evento.Id == id);
        }
    }
}
