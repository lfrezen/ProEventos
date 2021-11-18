using System;
using System.Threading.Tasks;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeneralPersist _generalPersist;
        private readonly IEventoPersist _eventoPersist;

        public EventoService(IGeneralPersist generalPersist, IEventoPersist eventoPersist)
        {
            _generalPersist = generalPersist;
            _eventoPersist = eventoPersist;
        }

        public async Task<Evento> Add(Evento model)
        {
            try
            {
                _generalPersist.Add<Evento>(model);
                if (await _generalPersist.SaveChangesAsync())
                    return await _eventoPersist.ObterEventoPorIdAsync(model.Id, false);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> Update(int id, Evento model)
        {
            try
            {
                var evento = await _eventoPersist.ObterEventoPorIdAsync(id, false);
                if (evento == null) return null;

                model.Id = evento.Id;
                _generalPersist.Update<Evento>(model);

                if (await _generalPersist.SaveChangesAsync())
                    return await _eventoPersist.ObterEventoPorIdAsync(model.Id, false);

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

        public async Task<Evento[]> ObterTodosEventosAsync(bool incluirPalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.ObterTodosEventosAsync(incluirPalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> ObterTodosEventosPorTemaAsync(string tema, bool incluirPalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.ObterTodosEventosPorTemaAsync(tema, incluirPalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Evento> ObterEventoPorIdAsync(int id, bool incluirPalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersist.ObterEventoPorIdAsync(id, incluirPalestrantes);
                if (evento == null) return null;

                return evento;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}