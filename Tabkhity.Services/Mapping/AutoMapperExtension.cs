using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Tabkhity.Core.Entities;
using Tabkhity.Services.DTOs.Lunch;

namespace Tabkhity.Services.Mapping
{
    public static class AutoMapperExtension
    {
        public static void ConfigAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AssembleType));
        }

        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<Lunch, LunchToReturnDto>().ReverseMap();
            }
        }
    }
}
