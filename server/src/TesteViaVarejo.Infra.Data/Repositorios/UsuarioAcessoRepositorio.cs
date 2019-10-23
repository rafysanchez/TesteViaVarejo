using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TesteViaVarejo.Domain.Entidades;
using TesteViaVarejo.Domain.Interfaces.Repositorios;
using TesteViaVarejo.Infra.Data.Config;

namespace TesteViaVarejo.Infra.Data.Repositorios
{
    public class UsuarioAcessoRepositorio : RepositorioBase<UsuarioAcesso>, IUsuarioAcessoRepositorio
    {
        public UsuarioAcessoRepositorio(Contexto contexto)
            : base(contexto)
        {

        }

        public UsuarioAcesso ObterUsuario(string usuario, string chaveAcesso)
        {
            return Db.UsuarioAcesso.FirstOrDefault(x => x.Usuario == usuario && x.ChaveAcesso == chaveAcesso);
        }

    }
}
