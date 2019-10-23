using Sage.Comtax.API.Domain.Core.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteViaVarejo.Domain.Entidades
{
    public class UsuarioAcesso : BaseEntidade<UsuarioAcesso>
    {
        public string Usuario { get; set; }
        public string ChaveAcesso { get; set; }

        public override bool EhValido()
        {
            return true;
        }
    }
}
