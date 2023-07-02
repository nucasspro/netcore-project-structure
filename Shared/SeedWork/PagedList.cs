namespace Shared.SeedWork;

public class PagedList<T> : List<T>
{
    private MetaData _metaData { get; }
    public MetaData GetMetaData() => _metaData;

    public PagedList(IEnumerable<T> items, long totalItems, int pageNumber, int pageSize)
    {
        _metaData = new MetaData
        {
            TotalItems = totalItems,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPages = (int) Math.Ceiling(totalItems/ (double) pageSize)
        };
        
        AddRange(items);
    }
}