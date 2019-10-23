using System;
using System.Collections.Generic;
using System.Text;
using TesteViaVarejo.Domain.Entidades;

namespace TesteViaVarejo.Domain.Interfaces.Servicos
{
    public interface IUsuarioAcessoServico
    {
        UsuarioAcesso ObterUsuario(string usuario, string chaveAcesso);
    }
}
