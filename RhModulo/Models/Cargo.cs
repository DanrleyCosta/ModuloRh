using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace RhModulo.Models
{
    public class Cargo
    {
        [Key]
        public int IdCargo { get; set; }
        public string NomeCargo { get; set; }
        public int LimiteHoraMes { get; set; }
        public int ValorHora { get; set; }
        public string Descricao { get; set; }

    }
}
