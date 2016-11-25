using AutoMapper;
using Gerenciador_de_Consultas_Médicas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gerenciador_de_Consultas_Médicas.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "DomainToViewModelMappingProfile";
            }
        }

        //protected override void Configure()
        //{
        //    Mapper.CreateMap<pacientes, PacientesViewModel>();
        //}
    }
}