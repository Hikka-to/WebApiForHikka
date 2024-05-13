﻿using AutoMapper;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Formats;
using WebApiForHikka.Dtos.Dto.Kinds;
using WebApiForHikka.Dtos.Dto.Periods;
using WebApiForHikka.Dtos.Dto.RestrictedRatings;
using WebApiForHikka.Dtos.Dto.SeoAddition;
using WebApiForHikka.Dtos.Dto.Sources;
using WebApiForHikka.Dtos.Dto.Status;
using WebApiForHikka.Dtos.Dto.Users;

namespace WebApiForHikka.Dtos.Helper;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
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





    }
}
