using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RhModulo.Models
{
    public class Registro
    {
        [Key]
        public int IdRegistro { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime DataHoraEntradaAlmoco { get; set; }
        public DateTime DataHoraSaidaAlmoco { get; set; }
        public DateTime DataHoraSaida { get; set; }
        public int MatriculaPf { get; set; }
        public int MatriculaPj { get; set; }
        public string Observacao { get; set; }
               
    }
}
