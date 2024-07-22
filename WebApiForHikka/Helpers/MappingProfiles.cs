using AutoMapper;
using WebApiForHikka.Domain;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.Relation.Relateds;
using WebApiForHikka.Dtos.Dto.Relation.Seasons;
using WebApiForHikka.Dtos.Dto.SeoAdditions;
using WebApiForHikka.Dtos.Dto.SharedDtos;
using WebApiForHikka.Dtos.Dto.Users;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AlternativeNames;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeGroups;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideoKinds;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideos;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Episodes;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.ExternalLinks;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Mediaplayers;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Providers;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.RelatedTypes;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserSettings;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Collections;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Countries;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Dubs;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Episodes;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Formats;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Kinds;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.LanguageMediaplayers;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Languages;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Periods;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.RestrictedRatings;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Sources;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Statuses;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Studios;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Tags;

namespace WebApiForHikka.WebApi.Helpers;

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
            op => op.MapFrom(v => v.PosterPath)).ForMember(
            c => c.Tags,
            op => op.MapFrom(v => v.Tags)).ForMember(
            c => c.Dubs,
            op => op.MapFrom(v => v.Dubs)).ForMember(
            c => c.Countries,
            op => op.MapFrom(v => v.Countries)).ForMember(
            c => c.RelatedAnimeGroups,
            op => op.MapFrom(v => v.RelatedAnimeGroups)).ForMember(
            c => c.SeasonAnimeGroups,
            op => op.MapFrom(v => v.SeasonAnimeGroups)).ForMember(
            c => c.SimilarAnimes,
            op => op.MapFrom(v => v.SimilarChildAnimes));

        CreateMap<CreateAnimeDto, Anime>().ForMember(
            c => c.Tags,
            op => op.Ignore()).ForMember(
            c => c.Dubs,
            op => op.Ignore()).ForMember(
            c => c.Countries,
            op => op.Ignore()).ForMember(
            c => c.SimilarChildAnimes,
            op => op.Ignore());

        CreateMap<UpdateAnimeDto, Anime>().ForMember(
            c => c.Tags,
            op => op.Ignore()).ForMember(
            c => c.Dubs,
            op => op.Ignore()).ForMember(
            c => c.Countries,
            op => op.Ignore()).ForMember(
            c => c.SimilarChildAnimes,
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

        //ExternalLink

        CreateMap<ExternalLink, GetExternalLinkDto>().ForMember(
            c => c.AnimeId,
            op => op.MapFrom(v => v.Anime.Id));

        CreateMap<CreateExternalLinkDto, ExternalLink>();

        CreateMap<UpdateExternalLinkDto, ExternalLink>();

        //RelatedType

        CreateMap<RelatedType, GetRelatedTypeDto>();

        CreateMap<CreateRelatedTypeDto, RelatedType>();

        CreateMap<UpdateRelatedTypeDto, RelatedType>();

        //AnimeGroup

        CreateMap<AnimeGroup, GetAnimeGroupDto>();

        CreateMap<CreateAnimeGroupDto, AnimeGroup>();

        CreateMap<UpdateAnimeGroupDto, AnimeGroup>();

        //Related

        CreateMap<Related, GetRelatedDto>().ForMember(
            c => c.AnimeId,
            op => op.MapFrom(v => v.FirstId)).ForMember(
            c => c.AnimeGroupId,
            op => op.MapFrom(v => v.SecondId)).ForMember(
            c => c.RelatedTypeId,
            op => op.MapFrom(v => v.RelatedType.Id));

        CreateMap<CreateRelatedDto, Related>().ForMember(
            c => c.FirstId,
            op => op.MapFrom(v => v.AnimeId)).ForMember(
            c => c.SecondId,
            op => op.MapFrom(v => v.AnimeGroupId));

        CreateMap<UpdateRelatedDto, Related>().ForMember(
            c => c.FirstId,
            op => op.MapFrom(v => v.AnimeId)).ForMember(
            c => c.SecondId,
            op => op.MapFrom(v => v.AnimeGroupId));

        //Season

        CreateMap<Season, GetSeasonDto>();

        CreateMap<CreateSeasonDto, Season>();

        CreateMap<UpdateSeasonDto, Season>();

        //Episode

        CreateMap<Episode, GetEpisodeDto>();

        CreateMap<CreateEpisodeDto, Episode>();

        CreateMap<UpdateEpisodeDto, Episode>();

        //EpisodeImage

        CreateMap<EpisodeImage, GetEpisodeImageDto>();

        CreateMap<CreateEpisodeImageDto, EpisodeImage>();

        CreateMap<UpdateEpisodeImageDto, EpisodeImage>();

        //Collection

        CreateMap<Collection, GetCollectionDto>();

        CreateMap<CreateCollectionDto, Collection>();

        CreateMap<UpdateCollectionDto, Collection>();
        
        // Language
        
        CreateMap<Language, GetLanguageDto>();
        
        CreateMap<CreateLanguageDto, Language>();

        CreateMap<UpdateLanguageDto, Language>();
        
        // LanguageMediaplayer
        
        CreateMap<LanguageMediaplayer, GetLanguageMediaplayerDto>();
        
        CreateMap<CreateLanguageMediaplayerDto, LanguageMediaplayer>();

        CreateMap<UpdateLanguageMediaplayerDto, LanguageMediaplayer>();
        
        // Provider
        
        CreateMap<Provider, GetProviderDto>();
        
        CreateMap<CreateProviderDto, Provider>();

        CreateMap<UpdateProviderDto, Provider>();
        
        // UserSetting
        CreateMap<UserSetting, GetUserSettingDto>();
        
        CreateMap<CreateUserSettingDto, UserSetting>();

        CreateMap<UpdateUserSettingDto, UserSetting>();
        
    }
}