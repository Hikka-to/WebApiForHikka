﻿using AutoMapper;
using WebApiForHikka.Application.Relation.Seasons;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Dtos.Dto.Relation.Seasons;
using WebApiForHikka.WebApi.Shared;

namespace WebApiForHikka.WebApi.Controllers.Relation;

public class SeasonController(
    ISeasonRelationService crudService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CrudController<
        GetSeasonDto,
        UpdateSeasonDto,
        CreateSeasonDto,
        ISeasonRelationService,
        Season
    >(crudService, mapper, httpContextAccessor);