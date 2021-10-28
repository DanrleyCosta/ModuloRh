using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RhModulo.Models
{
    public class Setor
    {
        [Key]
        public int IdSetor { get; set; }
        public string Nome { get; set; }
        public string DescSetor { get; set; }
        public int IdOrganizacao { get; set; }
     
    }
}
