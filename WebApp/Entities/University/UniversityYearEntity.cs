using System;
using System.Collections.Generic;

namespace WebApp.Models.University;

public partial class UniversityYearEntity
{
    public int? UniversityId { get; set; }

    public int? Year { get; set; }

    public int? NumStudents { get; set; }

    public double? StudentStaffRatio { get; set; }

    public int? PctInternationalStudents { get; set; }

    public int? PctFemaleStudents { get; set; }

    public virtual UniversityEntity? University { get; set; }
}
