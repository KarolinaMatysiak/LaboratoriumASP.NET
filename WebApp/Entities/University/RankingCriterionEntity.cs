using System;
using System.Collections.Generic;

namespace WebApp.Models.University;

public partial class RankingCriterionEntity
{
    public int Id { get; set; }

    public int? RankingSystemId { get; set; }

    public string? CriteriaName { get; set; }

    public virtual RankingSystemEntity? RankingSystem { get; set; }
    
    public virtual IEnumerable<UniversityRankingYearEntity>? UniversityRankingYearEntity { get; set; }
}
