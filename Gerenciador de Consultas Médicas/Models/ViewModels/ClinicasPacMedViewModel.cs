
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gerenciador_de_Consultas_Médicas.Models
{
    public class ClinicasPacMedViewModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe seu nome")]
        public string nomePaciente { get; set; }

        [Required(ErrorMessage = "Informe seu endereço")]
        [Display(Name = "Endereço")]
        public string enderecoPaciente { get; set; }

        [Required(ErrorMessage = "Informe sua cidade")]
        [Display(Name = "Cidade")]
        public string cidadePaciente { get; set; }

        public int idClinica { get; set; }

        [Display(Name = "Nome")]
        public string nomeClinica { get; set; }

        [Display(Name = "Endereco")]
        public string enderecoClinica { get; set; }

        [Display(Name = "Bairro")]
        public string bairroClinica { get; set; }

        [Display(Name = "CEP")]
        public string cepClinica { get; set; }

        [Display(Name = "Cidade")]
        public string cidadeClinica { get; set; }

        [Display(Name = "Estado")]
        public string estadoClinica { get; set; }

        [Display(Name = "Email")]
        public string emailClinica { get; set; }

        [Display(Name = "Telefone")]
        public string telefoneClinica { get; set; }

        [Display(Name = "Horár. Atendimento")]
        public string hrAtendimento { get; set; }

        [Display(Name = "Especialidades")]
        public string especialidades { get; set; }

        [Display(Name = "Convênios")]
        public string convenios { get; set; }
    }

    public class infParaAgendaEConsultaViewModel
    {
        public int idPaciente { get; set; }
        public int idClinica { get; set; }

        public int idMedico { get; set; }

        [Display(Name = "Nome do médico")]
        public string nomeMedico { get; set; }

        [Display(Name = "E-mail")]
        public string emailMedico { get; set; }

        [Display(Name = "Preço")]
        public string precoMedico { get; set; }

        [Display(Name = "Data")]
        public string data { get; set; }

        [Display(Name = "Horário")]
        public string horarios { get; set; }
    }

    public class perfilMedicoViewModel
    {
        public int idMedico { get; set; }
        [Display(Name = "Nome")]
        public string nome { get; set; }
        [Display(Name = "Especialidade")]
        public string especialidade { get; set; }
        [Display(Name = "E-mail")]
        public string emailMedico { get; set; }
        [Display(Name = "Preço")]
        public string preco { get; set; }
        [Display(Name = "Média")]
        public double media { get; set; }
        [Display(Name = "Comentários")]
        public string comentarios { get; set; }
        [Display(Name = "Notas")]
        public double notas { get; set; }
        [Display(Name = "Data avaliação")]
        public DateTime dataAvaliacao { get; set; }
        [Display(Name = "Nome do paciente")]
        public string nomePacAvaliador { get; set; }
    }

    public class agendaMedicoCadastroViewModel
    {

        [Key]
        public int idAgenda { get; set; }
        public int idMedico { get; set; }
        [Display(Name = "Horário de atendimento")]
        [DataType(DataType.Time)]
        public DateTime horarioAtendimento { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Início do atendimento")]
        public TimeSpan inicioAtendimento { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Fim do atendimento")]
        [GreaterThan("inicioAtendimento", ErrorMessage = "O horário de fim deve ser maior que o horário de início")]
        public TimeSpan fimAtendimento { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data")]
        public string data { get; set; }
    }

    //usado para cadastrar a avaliaçao
    public class infAvaliacaoViewModel
    {
        public int idMedico { get; set; }
        [Range(0, 5, ErrorMessage = "A nota deve ser de 0 a 5")]
        [Display(Name = "Nota", Description = "0 a 5", Prompt = "0 a 5")]
        public int nota { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentário")]
        public string comentario { get; set; }
        public double media { get; set; }
        public string data { get; set; }
    }
}