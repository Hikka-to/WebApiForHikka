using AutoMapper;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.SeoAdditions;
using WebApiForHikka.Dtos.Dto.SharedDtos;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AlternativeNames;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideoKinds;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideos;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Mediaplayers;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.RelatedTypes;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Countries;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Dubs;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Formats;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Kinds;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Periods;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.RestrictedRatings;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Sources;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Statuses;
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
            c => c.Kind,
            op => op.MapFrom(v => v.Kind)).ForMember(
            c => c.Status,
            op => op.MapFrom(v => v.Status)).ForMember(
            c => c.Period,
            op => op.MapFrom(v => v.Period)).ForMember(
            c => c.RestrictedRating,
            op => op.MapFrom(v => v.RestrictedRating)).ForMember(
            c => c.Source,
            op => op.MapFrom(v => v.Source)).ForMember(
            c => c.PosterPathUrl,
            op => op.MapFrom(v => v.PosterPath)
        ).ForMember(
            c => c.Tags,
            op => op.MapFrom(v => v.Tags)
        );

        CreateMap<CreateAnimeDto, Anime>().ForMember(
            c => c.Tags,
            op => op.Ignore());

        CreateMap<UpdateAnimeDto, Anime>().ForMember(
            c => c.Tags,
            op => op.Ignore());


        //AnimeBackdrop

        CreateMap<AnimeBackdrop, GetAnimeBackdropDto>().ForMember(
            c => c.AnimeId,
            op => op.MapFrom(v => v.Anime.Id)).ForMember(
            c => c.ImageUrl,
            op => op.MapFrom(v => v.Path)
        );

        CreateMap<CreateAnimeBackdropDto, AnimeBackdrop>();

        CreateMap<UpdateAnimeBackdropDto, AnimeBackdrop>();

        //AnimeVideoKind

        CreateMap<AnimeVideoKind, GetAnimeVideoKindDto>();

        CreateMap<CreateAnimeVideoKindDto, AnimeVideoKind>();

        CreateMap<UpdateAnimeVideoKindDto, AnimeVideoKind>();

        //AnimeVideo

        CreateMap<AnimeVideo, GetAnimeVideoDto>().ForMember(
            c => c.AnimeVideoKindId,
            op => op.MapFrom(v => v.AnimeVideoKind.Id));

        CreateMap<CreateAnimeVideoDto, AnimeVideo>();

        CreateMap<UpdateAnimeVideoDto, AnimeVideo>();

        //AlternativeName

        CreateMap<AlternativeName, GetAlternativeNameDto>().ForMember(
            c => c.AnimeId,
            op => op.MapFrom(v => v.Anime.Id));

        CreateMap<CreateAlternativeNameDto, AlternativeName>();

        CreateMap<UpdateAlternativeNameDto, AlternativeName>();

        //Relatedname

        CreateMap<RelatedType, GetRelatedTypeDto>();

        CreateMap<CreateRelatedTypeDto, RelatedType>();

        CreateMap<UpdateRelatedTypeDto, RelatedType>();

    }
}