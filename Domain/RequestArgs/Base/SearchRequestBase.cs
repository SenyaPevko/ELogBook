namespace Domain.RequestArgs.Base;

public abstract class SearchRequestBase
{
    public List<Guid>? Ids { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }

    public bool? GetAll { get; set; }
}