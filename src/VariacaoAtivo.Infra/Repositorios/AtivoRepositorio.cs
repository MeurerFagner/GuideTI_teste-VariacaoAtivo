using Microsoft.EntityFrameworkCore;
using VariacaoAtivo.Dominio.Entidade;
using VariacaoAtivo.Dominio.Interfaces.Infra.Respositorios;

namespace VariacaoAtivo.Infra.Repositorios
{
    public class AtivoRepositorio : RepositorioBase<Ativo>, IAtivoRepositorio
    {
        public AtivoRepositorio(Context context) : base(context)
        {
        }

        public async Task<Ativo?> ObtemAtivoPeloSimbolo(string symbol)
        {
            var ativo = await _context.Ativos.FirstOrDefaultAsync(a => a.Symbol == symbol.ToUpper());

            return ativo;
        }
    }
}
