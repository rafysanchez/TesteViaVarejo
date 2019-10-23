using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ngTesteViaVarejo.Controllers
{
    [Route("api/[controller]")]
    public class DemoController : Controller
    {
        private static string _urlBase;

        [HttpGet("[action]")]
        public IEnumerable<Amigo> ObterAmigos()
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile($"appsettings.json");
            var config = builder.Build();

            _urlBase = config.GetSection("API_Access:UrlBase").Value;
            List<Amigo> amigos = new List<Amigo>();

            //amigos.Add(new Amigo { AmigoId = 1, Nome = "Fabio", Distancia = 15.5 });
            //amigos.Add(new Amigo { AmigoId = 1, Nome = "Alexandre", Distancia = 13.5 });
            //amigos.Add(new Amigo { AmigoId = 1, Nome = "Bárbara", Distancia = 12.5 });
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage respToken = client.PostAsync(
                    _urlBase + String.Format("Usuario/Login/{0}/{1}", config.GetSection("API_Access:Usuario").Value,
                    config.GetSection("API_Access:ChaveAcesso").Value), null
                    ).Result;
                //_urlBase + "login", new StringContent(
                //    JsonConvert.SerializeObject(new
                //    {
                //        UsuarioAcessoId = config.GetSection("API_Access:UsuarioAcessoId").Value,
                //        ChaveAcesso = config.GetSection("API_Access:ChaveAcesso").Value
                //    }), Encoding.UTF8, "application/json")).Result;

                string conteudo =
                    respToken.Content.ReadAsStringAsync().Result;
                Console.WriteLine(conteudo);

                if (respToken.StatusCode == HttpStatusCode.OK)
                {
                    Token token = JsonConvert.DeserializeObject<Token>(conteudo);
                    if (token.Authenticated)
                    {
                        client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", token.AccessToken);

                        amigos = BuscaAmigos(client, 8);
                    }
                }
            }

            //var rng = new Random();
            return amigos;
        }


        private static List<Amigo> BuscaAmigos(HttpClient client, int amigoId)
        {
            var url = String.Format("{0}Amigo/LocalizarAmigosProximos/{1}", _urlBase, amigoId);
            HttpResponseMessage response = client.GetAsync(url).Result;

            List<Amigo> amigos = new List<Amigo>();


            if (response.StatusCode == HttpStatusCode.OK)
            {
                string resultado = response.Content.ReadAsStringAsync().Result;
                amigos = JsonConvert.DeserializeObject<List<Amigo>>(resultado);
            }

            return amigos;
        }

        public class Amigo
        {
            public string Nome { get; set; }
            public double Distancia { get; set; }
        }

        public class Token
        {
            public bool Authenticated { get; set; }
            public string Created { get; set; }
            public string Expiration { get; set; }
            public string AccessToken { get; set; }
            public string Message { get; set; }
        }
    }
}