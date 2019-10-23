using Sage.Comtax.API.Domain.Core.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TesteViaVarejo.Domain.Entidades
{
    public class Amigo : BaseEntidade<Amigo>
    {
        public string Nome { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [NotMapped]
        public double Distancia { get; set; }

        public override bool EhValido()
        {
            return true;
        }

        public Amigo(Int32 id)
        {
            this.Id = id;
        }
    }
}
