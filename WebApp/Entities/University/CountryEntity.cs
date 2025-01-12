using System;
using System.Collections.Generic;

namespace WebApp.Models.University;

public partial class CountryEntity
{
    public int Id { get; set; }

    public string? CountryName { get; set; }

    public virtual ICollection<UniversityEntity> Universities { get; set; } = new List<UniversityEntity>();
}
