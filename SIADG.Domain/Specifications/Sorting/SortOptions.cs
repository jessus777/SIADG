using SIADG.Toolkit.Text;

namespace SIADG.Domain.Specifications.Sorting;

public class SortOptions
{
    public SortOptions(string key, SortDirection? direction = null)
    {
        Key = key;
        Direction = direction ?? SortDirection.Asc;
    }

    public string Key { get; }
    public SortDirection Direction { get; }

    public string ToString(
        CaseConvention? keyConvention = null,
        CaseConvention directionConvention = CaseConvention.LowerCase,
        string separator = " "
        )
        => $"{Key.ApplyConvention(keyConvention)}{separator}{Direction.ToString().ApplyConvention(directionConvention)}";

    public static IEnumerable<SortOptions> FromString(string sortOptions)
    {
        if (string.IsNullOrEmpty(sortOptions))
            return Array.Empty<SortOptions>();

        return sortOptions
            .Split(",")
            .Select(s => s.Trim().Split(":"))
            .Where(array => array.Length is >= 1 and <= 2)
            .Select(array =>
            {
                var key = array[0].Trim();
                var direction = array.Length == 2 ? array[1].Trim().ToLower() : null;

                return new SortOptions(key, direction == "asc" ? SortDirection.Asc : SortDirection.Desc);
            });
    }
}