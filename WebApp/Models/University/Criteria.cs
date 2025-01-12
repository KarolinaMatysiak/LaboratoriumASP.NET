namespace WebApp.Models.University;

public class CriteriaUniversityModel
{
    public string UniversityName { get; set; }
    public string Country { get; set; }
}

public class CriteriaRecord
{
    public string? Name { get; set; }
    public Dictionary<int, int?> Data { get; set; }
}

public class CriteriaModel
{
    public CriteriaUniversityModel? CriteriaUniversity { get; set; }
    public IEnumerable<CriteriaRecord> CriteriaRecords { get; set; }
    public int? StartYear { get; set; }
}