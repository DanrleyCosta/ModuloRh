using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace RhModulo.Models
{
    public class FuncionarioPj
    {
        [Key]
        public int Matricula { get; set; }
        public string Cnpj { get; set; }
        public string Nome { get; set; }
        public DateTime DataAbertura { get; set; }
        public int IdCargo { get; set; }
        public int IdSetor { get; set; }
        
    }
}
