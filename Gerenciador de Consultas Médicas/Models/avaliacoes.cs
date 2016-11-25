//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gerenciador_de_Consultas_Médicas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class avaliacoes
    {
        [key]
        public int idAvaliacao { get; set; }
        [Required(ErrorMessage = "Informe a nota")]
        [Display(Name = "Notas")]
        public int notas { get; set; }
        [Display(Name = "Comentários")]
        public string comentarios { get; set; }
        [Display(Name = "Média")]
        public double media { get; set; }
        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        public System.DateTime data { get; set; }
        public int medicos_idMedico { get; set; }
        public int pacientes_idPaciente { get; set; }
    
        public virtual medicos medicos { get; set; }
        public virtual pacientes pacientes { get; set; }
    }
}
