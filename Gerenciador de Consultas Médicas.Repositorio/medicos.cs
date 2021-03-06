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

    public partial class medicos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public medicos()
        {
            this.consultas = new HashSet<consultas>();
            this.clinicas_has_medicos = new HashSet<clinicas_has_medicos>();
        }
    
        public int idMedico { get; set; }

        [Required(ErrorMessage = "Informe o nome do médico")]
        public string nome { get; set; }
        
        [Required(ErrorMessage = "Informe um e-mail")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
        public string email { get; set; }

        [Required(ErrorMessage = "Informe a senha de acesso")]
        [Display(Name = "Senha")]
        public string senhaAcesso { get; set; }
        public int idade { get; set; }
        public Nullable<int> notas { get; set; }
        public string comentários { get; set; }
        public string media { get; set; }
        public int especialidades_idEspecialidade { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<consultas> consultas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<clinicas_has_medicos> clinicas_has_medicos { get; set; }
        public virtual especialidades especialidades { get; set; }
    }
}
