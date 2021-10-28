using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace RhModulo.Models
{
    public class Contrato
    {
        [Key]
        public int IdContrato { get; set; }
        public string DescricaoContrato { get; set; }
        public int QuantHoraContratada { get; set; }
        public decimal ValorHoraContratada { get; set; }
        public int MatriculaPf { get; set; }
        public int MatriculaPj { get; set; }      
    }
}
