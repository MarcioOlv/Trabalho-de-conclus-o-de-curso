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
    using System.Web.Mvc;

    public partial class clinicas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public clinicas()
        {
            this.contasBancarias = new HashSet<contasBancarias>();
            this.consultas = new HashSet<consultas>();
            this.medicos = new HashSet<medicos>();
        }

        [Key]
        public int idClinica { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome da clínica")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Informe o CNPJ")]
        [Display(Name = "CNPJ")]
        public string cnpj { get; set; }

        [Required(ErrorMessage = "Informe o endereço")]
        [Display(Name = "Endereço")]
        public string endereco { get; set; }

        [Display(Name = "Complemento")]
        public string complemento { get; set; }

        [Required(ErrorMessage = "Informe o bairro")]
        [Display(Name = "Bairro")]
        public string bairro { get; set; }

        [Required(ErrorMessage = "Informe o CEP")]
        [Display(Name = "CEP")]
        public string cep { get; set; }

        [Required(ErrorMessage = "Informe a cidade")]
        [Display(Name = "Cidade")]
        public string cidade { get; set; }

        [Required(ErrorMessage = "Informe o estado")]
        [Display(Name = "Estado")]
        public string estadoClinica { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        [Required(ErrorMessage = "Informe o e-mail")]
        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Required(ErrorMessage = "Informe o telefone")]
        [Display(Name = "Telefone")]
        [Phone]
        public string telefone { get; set; }

        [Display(Name = "Fax")]
        public string fax { get; set; }

        [Required(ErrorMessage = "Informe o horário de atendimento")]
        [Display(Name = "Horário de atendimento")]
        public string hrAtendimento { get; set; }

        [Required(ErrorMessage = "Informe a duração da consulta")]
        [Display(Name = "Duração das consultas")]
        public int duracaoConsultas { get; set; }

        [Required(ErrorMessage = "Informe a(s) especialidade(s)")]
        [Display(Name = "Especialidade")]
        public string especialidades { get; set; }

        [Required(ErrorMessage = "Informe o convênio")]
        [Display(Name = "Convênio")]
        public string convenios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<contasBancarias> contasBancarias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<consultas> consultas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<medicos> medicos { get; set; }
    }
}
