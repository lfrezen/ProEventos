using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence
{
    public interface IPalestrantePersist
    {
        Task<Palestrante[]> ObterTodosPalestrantesPorNomeAsync(string nome, bool incluirEventos);
        Task<Palestrante[]> ObterTodosPalestrantesAsync(bool incluirEventos);
        Task<Palestrante> ObterPalestrantePorIdAsync(int id, bool incluirEventos);
    }
}