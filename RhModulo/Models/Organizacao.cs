using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace RhModulo.Models
{
    public class Organizacao
    {
        [Key]
        public int IdOrganizacao { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public int NumEndereco { get; set; }
        public string Cep { get; set; }
        public string Uf { get; set; }       
        
    }
}
