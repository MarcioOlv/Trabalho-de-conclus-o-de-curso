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
    
    public partial class contasBancarias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public contasBancarias()
        {
            this.pagamentos = new HashSet<pagamentos>();
        }
    
        public int idContasBancarias { get; set; }
        public string agencia { get; set; }
        public string digitoAgencia { get; set; }
        public string cedente { get; set; }
        public string digitoCedente { get; set; }
        public string carteira { get; set; }
        public string contrato { get; set; }
        public string convenio { get; set; }
        public int clinicas_idClinica { get; set; }
    
        public virtual clinicas clinicas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pagamentos> pagamentos { get; set; }
    }
}
