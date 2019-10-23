using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using TesteViaVarejo.Domain.Entidades;
using TesteViaVarejo.Domain.Entidades.ValuesObjects;
using TesteViaVarejo.Domain.Interfaces.Servicos;

namespace TesteViaVarejo.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioAcessoServico _iUsuarioAcessoServico;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfig _tokenConfig;

        public UsuarioController(IUsuarioAcessoServico usuarioAcessoServico, SigningConfigurations signingConfigurations, TokenConfig tokenConfig)
        {
            _iUsuarioAcessoServico = usuarioAcessoServico;
            _signingConfigurations = signingConfigurations;
            _tokenConfig = tokenConfig;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("Login/{usuario}/{chaveAcesso}")]
        public object Login(string usuario, string chaveAcesso)
        {

            UsuarioAcesso usuarioBase = null;
            bool credenciaisValidas = false;
            if (usuario != null && !String.IsNullOrWhiteSpace(usuario) && !String.IsNullOrWhiteSpace(chaveAcesso))
            {
                usuarioBase = _iUsuarioAcessoServico.ObterUsuario(usuario, chaveAcesso);
                credenciaisValidas = (usuarioBase != null);
            }

            if (credenciaisValidas)
            {

                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(usuarioBase.Usuario, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuarioBase.Id.ToString())
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(_tokenConfig.Segundos);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = _tokenConfig.Emissor,
                    Audience = _tokenConfig.Audiencia,
                    SigningCredentials = _signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK"
                };
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
        }

    }
}