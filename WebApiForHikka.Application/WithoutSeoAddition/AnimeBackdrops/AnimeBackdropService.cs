﻿using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;

public class AnimeBackdropService(IAnimeBackdropRepository repository)
    : CrudService<AnimeBackdrop, IAnimeBackdropRepository>(repository),
    IAnimeBackdropService;
