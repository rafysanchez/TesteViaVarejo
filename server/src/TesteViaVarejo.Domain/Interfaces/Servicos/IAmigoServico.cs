using System;
using System.Collections.Generic;
using TesteViaVarejo.Domain.Entidades.ViewModel;

namespace TesteViaVarejo.Domain.Interfaces.Servicos
{
    public interface IAmigoServico
    {
        List<AmigoViewModel> LocalizarAmigosProximos(Int32 id);
    }
}
