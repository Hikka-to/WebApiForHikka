﻿using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.Providers;

public interface IProviderRepository : ICrudRepository<Provider>
{
}