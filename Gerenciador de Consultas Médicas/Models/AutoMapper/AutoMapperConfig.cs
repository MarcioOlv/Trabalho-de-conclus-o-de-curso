using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gerenciador_de_Consultas_Médicas.Mappers
{
    public class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }
        public static void RegisterMappings()
        {
            //AutoMapperConfig.Mapper = new MapperConfiguration((mapper) =>
            //{
            //    mapper.AddProfile<DomainToViewModelMappingProfile>();
            //    mapper.AddProfile<ViewModelToDomainMappingProfile>();
            //});
        }
    }
}