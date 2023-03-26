using Microsoft.EntityFrameworkCore;
using VariacaoAtivo.Dominio.Entidade;
using VariacaoAtivo.Dominio.Interfaces.Infra.Respositorios;

namespace VariacaoAtivo.Infra.Repositorios
{
    public class PregaoRepositorio: RepositorioBase<Pregao>, IPregaoRepositorio
    {
        public PregaoRepositorio(Context context) : base(context)
        {
        }

        public async Task<Pregao?> ObtemPorAtivoData(int id, DateTime dataPregao)
        {
            var pregao = await _context.Pregoes
                .FirstOrDefaultAsync(p => p.IdAtivo == id && p.DataPregao.Date == dataPregao.Date);

            return pregao;
        }

        public async Task<IEnumerable<Pregao>> SelecionaUltimosTritaPregoesDoAtivo(string ativo)
        {
            var pregoes = await _context.Pregoes
                .Include(i => i.Ativo)
                .Where(w => w.Ativo.Symbol == ativo
                         && w.Open.HasValue)
                .OrderBy(o => o.DataPregao)
                .ToListAsync();
                

            return pregoes.TakeLast(30);                            
        }
    }
}
