﻿using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.Reviews;

public interface IReviewRepository : ICrudRepository<Review>
{
}