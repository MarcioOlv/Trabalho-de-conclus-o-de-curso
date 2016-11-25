//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gerenciador_de_Consultas_Médicas.Repositorio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class convenios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public convenios()
        {
            this.pagamentos = new HashSet<pagamentos>();
        }

        [Key]
        public int idConvenio { get; set; }

        [Required(ErrorMessage = "Informe o nome do convênio")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Informe o nome do plano de saúde")]
        [Display(Name = "Plano de saúde")]
        public string planoSaude { get; set; }

        [Display(Name = "Inicio do plano")]
        public System.DateTime inicioPlano { get; set; }
        public int telefone { get; set; }

        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
        public string email { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime validade { get; set; }
        public string rede { get; set; }
        public string acomodacao { get; set; }
        public string site { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pagamentos> pagamentos { get; set; }
    }
}
