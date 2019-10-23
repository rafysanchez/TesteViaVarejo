using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TesteViaVarejo.Domain.Entidades;
using TesteViaVarejo.Domain.Interfaces.Servicos;

namespace TesteViaVarejo.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AmigoController : Controller
    {
        private readonly IAmigoServico _amigoServico;
        public AmigoController(IAmigoServico amigoServico)
        {
            _amigoServico = amigoServico;
        }

        [Authorize("Bearer")]
        [HttpGet("LocalizarAmigosProximos/{id}")]
        public object LocalizarAmigosProximos(Int32 id)
        {
            var proximos = _amigoServico.LocalizarAmigosProximos(id);

            return Ok(proximos);
        }
    }
}