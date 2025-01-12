using System;
using System.Collections.Generic;

namespace WebApp.Models.University;

public partial class RankingSystemEntity
{
    public int Id { get; set; }

    public string? SystemName { get; set; }

    public virtual ICollection<RankingCriterionEntity> RankingCriteria { get; set; } = new List<RankingCriterionEntity>();
}
