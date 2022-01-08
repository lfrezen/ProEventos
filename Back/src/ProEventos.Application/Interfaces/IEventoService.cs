using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Interfaces
{
    public interface IEventoService
    {
        Task<EventoDto> Add(EventoDto model);
        Task<EventoDto> Update(int id, EventoDto model);
        Task<bool> Delete(int id);
        Task<EventoDto[]> ObterTodosEventosAsync(bool incluirPalestrantes = false);
        Task<EventoDto[]> ObterTodosEventosPorTemaAsync(string tema, bool incluirPalestrantes = false);
        Task<EventoDto> ObterEventoPorIdAsync(int id, bool incluirPalestrantes = false);
    }
}