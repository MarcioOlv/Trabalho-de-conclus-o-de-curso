using AutoMapper;
using Gerenciador_de_Consultas_Médicas;
using Gerenciador_de_Consultas_Médicas.Models;
using Gerenciador_de_Consultas_Médicas.Models.ViewModels;

namespace Gerenciador_de_Consultas_Médicas.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "ViewModelToDomainMappingProfile";
            }
        }

        protected override void Configure()
        {
            //Mapper.CreateMap<AgendaViewModel, agenda>();
        }
    }
}