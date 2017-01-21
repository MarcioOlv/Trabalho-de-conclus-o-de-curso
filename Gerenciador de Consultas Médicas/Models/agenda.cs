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

    public partial class agenda
    {
        [Key]
        public int idAgenda { get; set; }
        [Display(Name = "Data")]
        [Required(ErrorMessage = "Informe a data")]
        [DataType(DataType.Date)]
        public string data { get; set; }
        [Display(Name = "Horário atendimento")]
        [DataType(DataType.Time)]
        public string horarioAtendimento { get; set; }
        [Display(Name = "Horário agendado")]
        public string horarioAgendado { get; set; }
        public int medicos_idMedico { get; set; }

        public virtual medicos medicos { get; set; }
    }
}
