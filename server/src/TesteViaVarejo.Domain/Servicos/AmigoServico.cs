using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TesteViaVarejo.Domain.Entidades;
using TesteViaVarejo.Domain.Entidades.ViewModel;
using TesteViaVarejo.Domain.Interfaces.Repositorios;
using TesteViaVarejo.Domain.Interfaces.Servicos;

namespace TesteViaVarejo.Domain.Servicos
{

    public class AmigoServico : ServicoBase<Amigo>, IAmigoServico
    {
        private readonly IRepositorioBase<Amigo> _iAmigoRepositorio;
        private readonly IRepositorioBase<CalculoHistoricoLog> _iCalculoHistoricoLogRepositorio;

        public AmigoServico(IRepositorioBase<Amigo> repositorio, IRepositorioBase<CalculoHistoricoLog> calculoHistoricoLogRepositorio)
            : base (repositorio)
        {
            _iAmigoRepositorio = repositorio;
            _iCalculoHistoricoLogRepositorio = calculoHistoricoLogRepositorio;
        }

        public List<AmigoViewModel> LocalizarAmigosProximos(Int32 id)
        {
            List<AmigoViewModel> listaComDistancia = new List<AmigoViewModel>();
            
            var amigoAtual = _iAmigoRepositorio.ObterPorId(id);
            var todosAmigos = _iAmigoRepositorio.ObterTodos().ToList();
            todosAmigos.Remove(amigoAtual);

            foreach (var amigo in todosAmigos)
            {
                AmigoViewModel tmpAmigo = new AmigoViewModel();
                tmpAmigo.Nome = amigo.Nome;
                tmpAmigo.Distancia = ObterDistancia(amigo.Latitude, amigo.Longitude, amigoAtual.Latitude, amigoAtual.Longitude);
                _iCalculoHistoricoLogRepositorio.Adicionar(new CalculoHistoricoLog { AmigoId= amigo.Id, LatitudeRef = amigoAtual.Latitude, LongitudeRef = amigoAtual.Longitude, LatitudeAmigo = amigo.Latitude, LongitudeAmigo = amigo.Longitude, Distancia = tmpAmigo.Distancia });
                listaComDistancia.Add(tmpAmigo);
            }

            var proximos = listaComDistancia.OrderBy(x => x.Distancia).Take(3);

            return proximos.ToList();
        }

        struct Distancia
        {
            public double RadianosOrigem;
            public double RadianosDestino;
            public double RadianosTheta;
            public double Seno;
            public double Coseno;
            public double Angulo;
            public double Milhas;
            public double Kilometros;
        }

        public double ObterDistancia(double lat1, double lon1, double lat2, double lon2)
        {

            Distancia calculo = new Distancia();
            calculo.RadianosOrigem = Math.PI * lat1 / 180;
            calculo.RadianosDestino = Math.PI * lat2 / 180;
            calculo.RadianosTheta = Math.PI * (lon1 - lon2) / 180;

            calculo.Seno = Math.Sin(calculo.RadianosOrigem) * Math.Sin(calculo.RadianosDestino);
            calculo.Coseno = Math.Cos(calculo.RadianosOrigem) * Math.Cos(calculo.RadianosDestino) * Math.Cos(calculo.RadianosTheta);
            calculo.Angulo = Math.Acos(calculo.Seno + calculo.Coseno);
            calculo.Milhas = calculo.Angulo * 180 / Math.PI * 60 * 1.1515;
            var Kilometros = calculo.Milhas * 1.609344;

            return Kilometros;

        }
    }
}
