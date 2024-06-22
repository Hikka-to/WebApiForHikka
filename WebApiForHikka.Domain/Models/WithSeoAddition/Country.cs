﻿using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Countries;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Country : ModelWithSeoAddition
{
    [StringLength(CountryNumberConstants.NameLenght)]
    public required string Name { get; set; }

    [StringLength(CountryNumberConstants.IconLenght)]
    public required string Icon { get; set; }

    public ICollection<Anime> Animes { get; } = [];
    public ICollection<CountryAnime> CountryAnimes { get; } = [];
}
