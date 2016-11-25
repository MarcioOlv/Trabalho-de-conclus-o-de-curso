using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gerenciador_de_Consultas_Médicas.Models
{
    public class AccountViewModel
    {
        [Key]
        public int idUsuario { get; set; }

        [Required(ErrorMessage = "Informe seu e-mail")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        [Display(Name = "E-mail")]
        public string emailUsuario { get; set; }

        [Required(ErrorMessage = "Digite sua senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string senhaUsuario { get; set; }

        [Required(ErrorMessage = "Digite a confirmação da senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        public string confirmarSenha { get; set; }

        [Required(ErrorMessage = "Informe seu CPF")]
        [Display(Name = "CPF")]
        public string cpf { get; set; }

        public int adm { get; set; }
    }
}
