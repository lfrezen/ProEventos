using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeneralPersist _generalPersist;
        private readonly IEventoPersist _eventoPersist;
        private readonly IMapper _mapper;

        public EventoService(
            IGeneralPersist generalPersist,
            IEventoPersist eventoPersist,
            IMapper mapper)
        {
            _generalPersist = generalPersist;
            _eventoPersist = eventoPersist;
            _mapper = mapper;
        }

        public async Task<EventoDto> Add(EventoDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);

                _generalPersist.Add<Evento>(evento);
                if (await _generalPersist.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventoPersist.ObterEventoPorIdAsync(evento.Id, false);

                    return _mapper.Map<EventoDto>(eventoRetorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> Update(int id, EventoDto model)
        {
            try
            {
                var evento = await _eventoPersist.ObterEventoPorIdAsync(id, false);
                if (evento == null) return null;

                _mapper.Map(model, evento);

                _generalPersist.Update<Evento>(evento);

                if (await _generalPersist.SaveChangesAsync())
                {
                    var retorno = await _eventoPersist.ObterEventoPorIdAsync(model.Id, false);

                    return _mapper.Map<EventoDto>(retorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var evento = await _eventoPersist.ObterEventoPorIdAsync(id, false);
                if (evento == null) throw new Exception("Evento para delete não encontrado.");

                _generalPersist.Delete<Evento>(evento);

                return await _generalPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> ObterTodosEventosAsync(bool incluirPalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.ObterTodosEventosAsync(incluirPalestrantes);
                if (eventos == null) return null;

                var eventosDto = _mapper.Map<EventoDto[]>(eventos);

                return eventosDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> ObterTodosEventosPorTemaAsync(string tema, bool incluirPalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.ObterTodosEventosPorTemaAsync(tema, incluirPalestrantes);
                if (eventos == null) return null;

                var eventosDto = _mapper.Map<EventoDto[]>(eventos);

                return eventosDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<EventoDto> ObterEventoPorIdAsync(int id, bool incluirPalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersist.ObterEventoPorIdAsync(id, incluirPalestrantes);
                if (evento == null) return null;

                var eventoDto = _mapper.Map<EventoDto>(evento);

                return eventoDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}