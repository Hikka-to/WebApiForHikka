﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiForHikka.Domain.Models;
public class Status : ModelWithSeoAddition
{
    public required string Name { get; set; }
    

}
