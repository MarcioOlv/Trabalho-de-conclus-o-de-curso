﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DominioContainer : DbContext
    {
        public DominioContainer()
            : base("name=DominioContainer")
        {
        }

        //public DominioContainer()
        //    : base("name=rhmssql03.clinicatcc.dbo")
        //{
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<pacientes> pacientesSet { get; set; }
        public virtual DbSet<medicos> medicosSet { get; set; }
        public virtual DbSet<clinicas> clinicasSet { get; set; }
        public virtual DbSet<pagamentos> pagamentosSet { get; set; }
        public virtual DbSet<convenios> conveniosSet { get; set; }
        public virtual DbSet<consultas> consultasSet { get; set; }
        public virtual DbSet<especialidades> especialidadesSet { get; set; }
        public virtual DbSet<avaliacoes> avaliacoes { get; set; }
        public virtual DbSet<contasBancarias> contasBancariasSet { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<agenda> agendaSet { get; set; }
    }
}
