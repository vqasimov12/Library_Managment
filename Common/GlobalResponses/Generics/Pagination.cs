namespace Common.GlobalResponses.Generics;

public class Pagination<T>
{
    public List<T> Data { get; set; }
    public int TotalCount { get; set; }

    public Pagination(List<T> data, int totalCount)
    {
        Data = data;
        TotalCount = totalCount;
    }
    public Pagination()
    {
        Data = [];
        TotalCount = 0;
    }
}