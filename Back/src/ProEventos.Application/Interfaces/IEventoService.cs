using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Application.Interfaces
{
    public interface IEventoService
    {
        Task<Evento> Add(Evento model);
        Task<Evento> Update(int id, Evento model);
        Task<bool> Delete(int id);
        Task<Evento[]> ObterTodosEventosAsync(bool incluirPalestrantes = false);
        Task<Evento[]> ObterTodosEventosPorTemaAsync(string tema, bool incluirPalestrantes = false);
        Task<Evento> ObterEventoPorIdAsync(int id, bool incluirPalestrantes = false);
    }
}