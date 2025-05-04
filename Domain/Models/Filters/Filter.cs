namespace Domain.Models.Filters;

public class Filter
{
    public string Field { get; set; } = null!;
    public FilterOperator Operator { get; set; }
    public object? Value { get; set; }
}