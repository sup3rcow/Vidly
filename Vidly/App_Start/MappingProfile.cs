using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //koristi reflection, i nazive proprtije kako bi sam pomapirao..
            Mapper.CreateMap<Customer, CustomerDto>();/*
                .ForMember(dto => dto.Id, m => m.MapFrom(c => c.Id))
                .ForMember(dto => dto.Name, m => m.MapFrom(c => c.Name))
                .ForMember(dto => dto.IsSubscribedToNewsLetter, m => m.MapFrom(c => c.IsSubscribedToNewsLetter))
                .ForMember(dto => dto.MembershipTypeId, m => m.MapFrom(c => c.MembershipTypeId))
                .ForMember(dto => dto.Birthdate, m => m.MapFrom(c => c.Birthdate));*/

            Mapper.CreateMap<CustomerDto, Customer>();/*
                .ForMember(c => c.Id, opt => opt.Ignore())//ovako mu kazes da zanemari id, npr kad radis post,
                                                          //ali izgleda da i bez ovoga zanemaruje id.              
                .ForMember(c => c.Name, m => m.MapFrom(dto => dto.Name))
                .ForMember(c => c.IsSubscribedToNewsLetter, m => m.MapFrom(dto => dto.IsSubscribedToNewsLetter))
                .ForMember(c => c.MembershipTypeId, m => m.MapFrom(dto => dto.MembershipTypeId))
                .ForMember(c => c.Birthdate, m => m.MapFrom(dto => dto.Birthdate))
                .ForMember(c => c.MembershipType, m => m.MapFrom(dto => new MembershipType()));*/

            Mapper.CreateMap<Genre, GenreDto>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();

            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MovieDto, Movie>()
                .ForMember(c => c.Id, opt => opt.Ignore());//ovako mu kazes da zanemari id, npr kad radis post,
                                                           //ali izgleda da i bez ovoga zanemaruje id.
        }


            //ako ti ne moze refleksijom pomapiati, ili ne zelis sve mapirati.. onda rucno raspise, za to pogledaj
            //dokumentaciju od AutoMapper-a
    }
}
