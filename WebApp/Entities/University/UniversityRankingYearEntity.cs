using System;
using System.Collections.Generic;

namespace WebApp.Models.University;

public partial class UniversityRankingYearEntity
{
    public int? UniversityId { get; set; }

    public int? RankingCriteriaId { get; set; }

    public int? Year { get; set; }

    public int? Score { get; set; }

    public virtual RankingCriterionEntity? RankingCriteria { get; set; }

    public virtual UniversityEntity? University { get; set; }
}
