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
    
    public partial class pagamentos
    {
        public int idPagamento { get; set; }
        public double valor { get; set; }
        public System.DateTime data { get; set; }
        public Nullable<double> descontos { get; set; }
        public int consultas_idConsulta { get; set; }
        public int contasBancarias_idContasBancarias { get; set; }
    
        public virtual contasBancarias contasBancarias { get; set; }
    }
}
