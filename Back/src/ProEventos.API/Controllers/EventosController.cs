using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ProEventos.Application.Interfaces;
using System.Collections.Generic;
using ProEventos.Application.Dtos;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.ObterTodosEventosAsync(true);
                if (eventos == null) return NoContent();

                var eventosRetorno = new List<EventoDto>();

                foreach (var evento in eventos)
                {
                    eventosRetorno.Add(new EventoDto()
                    {
                        Id = evento.Id,
                        Local = evento.Local,
                        DataEvento = evento.DataEvento.ToString(),
                        QuantidadePessoas = evento.QuantidadePessoas,
                        ImagemUrl = evento.ImagemUrl,
                        Telefone = evento.Telefone,
                        Email = evento.Email
                    });
                }

                return Ok(eventosRetorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar eventos. Error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.ObterEventoPorIdAsync(id, true);
                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar eventos. Error: {ex.Message}");
            }
        }

        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var eventos = await _eventoService.ObterTodosEventosPorTemaAsync(tema, true);
                if (eventos == null) return NoContent();

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar eventos. Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
                var evento = await _eventoService.Add(model);
                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar eventos. Error: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, EventoDto model)
        {
            try
            {
                var evento = await _eventoService.Update(id, model);
                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar eventos. Error: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var evento = await _eventoService.ObterEventoPorIdAsync(id, true);
                if (evento == null) return NoContent();

                return await _eventoService.Delete(id) ?
                    Ok("Deletado") :
                    throw new Exception("Ocorreu um problema não específico ao tentar deletar o evento.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar eventos. Error: {ex.Message}");
            }
        }
    }
}
