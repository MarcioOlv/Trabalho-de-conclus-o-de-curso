using AutoMapper;
using Gerenciador_de_Consultas_Médicas;
using Gerenciador_de_Consultas_Médicas.Models;

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

        //protected override void Configure()
        //{
        //    Mapper.CreateMap<PacientesViewModel, pacientes>();
        //}
    }
}