using Domain.Models.Filters;

namespace Domain.RequestArgs.SearchRequest;

public class SearchRequest
{
    public List<Filter> Filters { get; set; } = new();
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; }
    public int? Page { get; set; }
    public int? PageSize { get; set; }
}