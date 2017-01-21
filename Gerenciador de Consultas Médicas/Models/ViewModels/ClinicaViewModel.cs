using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gerenciador_de_Consultas_Médicas.Models.ViewModels
{
    public class ClinicaViewModel
    {
        [Required(ErrorMessage = "Informe a(s) especialidade(s)")]
        [Display(Name = "Especialidade")]
        public MultiSelectList listaEspecialidades { get; set; }

        [Required(ErrorMessage = "Informe o convênio")]
        [Display(Name = "Convênio")]
        public MultiSelectList listaConvenios { get; set; }
    }

}