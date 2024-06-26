using AutoMapper;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.Countries;
using WebApiForHikka.Dtos.Dto.Formats;
using WebApiForHikka.Dtos.Dto.Kinds;
using WebApiForHikka.Dtos.Dto.Periods;
using WebApiForHikka.Dtos.Dto.RestrictedRatings;
using WebApiForHikka.Dtos.Dto.SeoAddition;
using WebApiForHikka.Dtos.Dto.SharedDtos;
using WebApiForHikka.Dtos.Dto.Sources;
using WebApiForHikka.Dtos.Dto.Status;
using WebApiForHikka.Dtos.Dto.Statuses;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Mediaplayers;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Dubs;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Studios;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Tags;

namespace WebApiForHikka.WebApi.Helper;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        //Shared
        CreateMap<FilterPaginationDto, FilterPagination>();

        //User
        CreateMap<User, GetUserDto>();

        CreateMap<User, UpdateUserDto>();

        CreateMap<UpdateUserDto, User>();

        //SeoAddition
        CreateMap<SeoAddition, GetSeoAdditionDto>();

        CreateMap<CreateSeoAdditionDto, SeoAddition>();

        CreateMap<UpdateSeoAdditionDto, SeoAddition>();

        //Status

        CreateMap<Status, GetStatusDto>();

        CreateMap<CreateStatusDto, Status>();

        CreateMap<UpdateStatusDto, Status>();

        //Source

        CreateMap<Source, GetSourceDto>();

        CreateMap<CreateSourceDto, Source>();

        CreateMap<UpdateSourceDto, Source>();

        //RestrictedRating

        CreateMap<RestrictedRating, GetRestrictedRatingDto>();

        CreateMap<CreateRestrictedRatingDto, RestrictedRating>();

        CreateMap<UpdateRestrictedRatingDto, RestrictedRating>();

        //Period

        CreateMap<Period, GetPeriodDto>();

        CreateMap<CreatePeriodDto, Period>();

        CreateMap<UpdatePeriodDto, Period>();

        //Kind

        CreateMap<Kind, GetKindDto>();

        CreateMap<CreateKindDto, Kind>();

        CreateMap<UpdateKindDto, Kind>();

        //Format

        CreateMap<Format, GetFormatDto>();

        CreateMap<CreateFormatDto, Format>();

        CreateMap<UpdateFormatDto, Format>();

        //Tag

        CreateMap<Tag, GetTagDto>().ForMember(
            c => c.ParentTagId,
            op => op.MapFrom(v => v.ParentTag!.Id));

        CreateMap<CreateTagDto, Tag>();

        CreateMap<UpdateTagDto, Tag>();

        //Country

        CreateMap<Country, GetCountryDto>();

        CreateMap<CreateCountryDto, Country>();

        CreateMap<UpdateCountryDto, Country>();

        //Studio

        CreateMap<Studio, GetStudioDto>();

        CreateMap<CreateStudioDto, Studio>();

        CreateMap<UpdateStudioDto, Studio>();

        //Dub

        CreateMap<Dub, GetDubDto>();

        CreateMap<CreateDubDto, Dub>();

        CreateMap<UpdateDubDto, Dub>();


        //Mediaplayer

        CreateMap<Mediaplayer, GetMediaplayerDto>();

        CreateMap<CreateMediaplayerDto, Mediaplayer>();

        CreateMap<UpdateMediaplayerDto, Mediaplayer>();


        //Anime

        CreateMap<Anime, GetAnimeDto>().ForMember(
            c => c.KindId,
            op => op.MapFrom(v => v.Kind!.Id)).ForMember(
            c => c.StatusId,
            op => op.MapFrom(v => v.Status!.Id)).ForMember(
            c => c.PeriodId,
            op => op.MapFrom(v => v.Period!.Id)).ForMember(
            c => c.RestrictedRatingId,
            op => op.MapFrom(v => v.RestrictedRating!.Id)).ForMember(
            c => c.SourceId,
            op => op.MapFrom(v => v.Source!.Id));

        CreateMap<CreateAnimeDto, Anime>();

        CreateMap<UpdateAnimeDto, Anime>();
    }
}