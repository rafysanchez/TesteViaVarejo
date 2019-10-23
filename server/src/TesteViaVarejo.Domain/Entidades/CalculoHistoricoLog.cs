using Sage.Comtax.API.Domain.Core.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteViaVarejo.Domain.Entidades
{
    public class CalculoHistoricoLog : BaseEntidade<CalculoHistoricoLog>
    {
        public Int32 AmigoId { get; set; }
        public double LatitudeRef { get; set; }
        public double LongitudeRef { get; set; }
        public double LatitudeAmigo { get; set; }
        public double LongitudeAmigo { get; set; }
        public double Distancia { get; set; }

        public override bool EhValido()
        {
            return true;
        }
    }
}
