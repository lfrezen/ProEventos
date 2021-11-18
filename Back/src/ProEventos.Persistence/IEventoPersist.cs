using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence
{
    public interface IEventoPersist
    {
        Task<Evento[]> ObterTodosEventosPorTemaAsync(string tema, bool incluirPalestrantes = false);
        Task<Evento[]> ObterTodosEventosAsync(bool incluirPalestrantes = false);
        Task<Evento> ObterEventoPorIdAsync(int id, bool incluirPalestrantes = false);
    }
}