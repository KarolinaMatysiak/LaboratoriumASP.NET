using System;
using System.Collections.Generic;

namespace WebApp.Models.University;

public partial class UniversityEntity
{
    public int Id { get; set; }

    public int? CountryId { get; set; }

    public string? UniversityName { get; set; }

    public virtual CountryEntity? Country { get; set; }
}
