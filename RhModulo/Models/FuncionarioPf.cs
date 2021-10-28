using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace RhModulo.Models
{
    public class FuncionarioPf
    {
        [Key]
        public int Matricula { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int IdCargo { get; set; }
        public int IdSetor { get; set; }        
    }
}
