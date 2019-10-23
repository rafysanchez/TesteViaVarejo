using System;
using System.Collections.Generic;
using System.Text;
using TesteViaVarejo.Domain.Entidades;
using TesteViaVarejo.Domain.Interfaces.Repositorios;
using TesteViaVarejo.Domain.Interfaces.Servicos;

namespace TesteViaVarejo.Domain.Servicos
{
    public class UsuarioAcessoServico : ServicoBase<UsuarioAcesso>, IUsuarioAcessoServico
    {
        private readonly IUsuarioAcessoRepositorio _iUsuarioAcessoRepositorio;
        private readonly IRepositorioBase<UsuarioAcesso> _iRepositorioBase;

        public UsuarioAcessoServico(IRepositorioBase<UsuarioAcesso> repositorio, IUsuarioAcessoRepositorio iUsuarioAcessoRepositorio)
            :base(repositorio)
        {
            _iUsuarioAcessoRepositorio = iUsuarioAcessoRepositorio;
            _iRepositorioBase = repositorio;
        }

        public UsuarioAcesso ObterUsuario(string usuario, string chaveAcesso)
        {
            return _iUsuarioAcessoRepositorio.ObterUsuario(usuario, chaveAcesso);
        }

    }
}
