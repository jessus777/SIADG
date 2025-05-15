using SIADG.Toolkit.Text;

namespace SIADG.Domain.Specifications.Sorting;

public static class SortOptionsExtensions
{
    public static IEnumerable<SortOptions> ApplyCaseConvention(
        this IEnumerable<SortOptions> sortOptions,
        CaseConvention? caseConvention = null
        )
        => sortOptions.Select(
            option =>
                caseConvention.HasValue && option.Key.SatisfiesConvention(caseConvention.Value)
                    ? option
                    : new SortOptions(option.Key.ApplyConvention(caseConvention), option.Direction)
            );
}