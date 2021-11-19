using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexts;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly ProEventosContext _context;

        public PalestrantePersist(ProEventosContext context) => _context = context;

        public async Task<Palestrante[]> ObterTodosPalestrantesPorNomeAsync(string nome, bool incluirEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);

            if (incluirEventos)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> ObterTodosPalestrantesAsync(bool incluirEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);

            if (incluirEventos)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query
                .AsNoTracking()
                .OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> ObterPalestrantePorIdAsync(int id, bool incluirEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);

            if (incluirEventos)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .Where(p => p.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}