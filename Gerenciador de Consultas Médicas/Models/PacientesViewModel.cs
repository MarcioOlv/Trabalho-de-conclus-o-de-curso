using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gerenciador_de_Consultas_Médicas.Models
{
    public class PacientesViewModel
    {

        [Key]
        public int idPaciente { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe seu nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Informe seu CPF")]
        [Display(Name = "CPF")]
        public string cpf { get; set; }

        [Required(ErrorMessage = "Informe seu endereço")]
        [Display(Name = "Endereço")]
        public string endereco { get; set; }

        [Required(ErrorMessage = "Informe sua cidade")]
        [Display(Name = "Cidade")]
        public string cidade { get; set; }

        [Required(ErrorMessage = "Informe seu estado")]
        [Display(Name = "Estado")]
        public string estado { get; set; }

        [Required(ErrorMessage = "Informe seu telefone")]
        [Display(Name = "Telefone")]
        public string telefone { get; set; }

        [Display(Name = "Celular")]
        public string celular { get; set; }

        [Required(ErrorMessage = "Informe seu e-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Required(ErrorMessage = "Digite sua senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string senha { get; set; }

        //[Compare("senha", ErrorMessage = "As senhas não conferem")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        public string confirmarSenha { get; set; }

        [Display(Name = "Convênio")]
        public int convenios_idConvenio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<consultas> consultas { get; set; }

        [Display(Name = "Convênio")]
        public virtual convenios convenios { get; set; }
    }
}