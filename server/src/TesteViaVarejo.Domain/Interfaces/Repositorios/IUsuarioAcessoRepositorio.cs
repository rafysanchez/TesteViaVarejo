using System;
using System.Collections.Generic;
using System.Text;
using TesteViaVarejo.Domain.Entidades;

namespace TesteViaVarejo.Domain.Interfaces.Repositorios
{
    public interface IUsuarioAcessoRepositorio :IRepositorioBase<UsuarioAcesso>, IDisposable
    {
        UsuarioAcesso ObterUsuario(string usuario, string chaveAcesso);
    }
}
