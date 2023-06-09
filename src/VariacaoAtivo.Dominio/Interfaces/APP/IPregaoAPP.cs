﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VariacaoAtivo.Dominio.DTO;

namespace VariacaoAtivo.Dominio.Interfaces.APP
{
    public interface IPregaoAPP
    {
        Task<IEnumerable<VariacaoPrecoDoAtivoDTO>> ConsultaVariacaoDePrecoDoAtivo(string ativoSymbol);
    }
}
