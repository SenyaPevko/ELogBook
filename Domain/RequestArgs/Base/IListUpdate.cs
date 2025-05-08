namespace Domain.RequestArgs.Base;

public interface IListUpdate<TItem>
{
    public List<TItem>? Add { get; set; }
    public List<TItem>? Remove { get; set; }
}