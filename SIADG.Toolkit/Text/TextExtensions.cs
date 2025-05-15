using System.Text;
using System.Text.RegularExpressions;
using CaseExtensions;

namespace SIADG.Toolkit.Text;

public static class TextExtensions
{
    private static readonly Dictionary<CaseConvention, string> CaseConventionPatterns = new()
    {
        {CaseConvention.LowerCase, "^[a-z\\d\\s]+$"},
        {CaseConvention.UpperCase, "^[A-Z\\d\\s]+$"},
        {CaseConvention.CamelCase, "^[a-z][a-zA-Z\\d]*$"},
        {CaseConvention.PascalCase, "^[A-Z][a-zA-Z\\d]*$"},
        {CaseConvention.LowerKebabCase, "^[a-z]+(-[a-z\\d]+)*$"},
        {CaseConvention.UpperKebabCase, "^[A-Z\\d]+(-[A-Z\\d]+)*$"},
        {CaseConvention.LowerSnakeCase, "^[a-z\\d]+(_[a-z\\d]+)*$"},
        {CaseConvention.UpperSnakeCase, "^[A-Z\\d]+(_[A-Z\\d]+)*$"},
        {CaseConvention.TrainCase, "^[A-Z][a-z\\d]+(-[A-Z][a-z\\d]+)*$"}
    };
    
    public static Encoding DefaultEncoding { get; set; } = Encoding.UTF8;

    public static bool SatisfiesConvention(this string str, CaseConvention caseConvention)
    {
        if (!CaseConventionPatterns.TryGetValue(caseConvention, out var pattern))
            return false;

        var regex = new Regex(pattern);
        return regex.IsMatch(str);
    }
    
    public static string ApplyConvention(this string str, CaseConvention? convention)
        => convention switch
        {
            CaseConvention.LowerCase => str.ToLower(),
            CaseConvention.UpperCase => str.ToUpper(),
            CaseConvention.CamelCase => str.ToCamelCase(),
            CaseConvention.PascalCase => str.ToPascalCase(),
            CaseConvention.LowerKebabCase => str.ToKebabCase(),
            CaseConvention.UpperKebabCase => str.ToKebabCase().ToUpper(),
            CaseConvention.LowerSnakeCase => str.ToSnakeCase(),
            CaseConvention.UpperSnakeCase => str.ToSnakeCase().ToUpper(),
            CaseConvention.TrainCase => str.ToTrainCase(),
            _ => str
        };
    
    public static string EncodeBase64(this string str, Encoding? encoding = null)
    {
        var enc = encoding ?? DefaultEncoding;
        return Convert.ToBase64String(enc.GetBytes(str));
    }
    
    public static string DecodeBase64(this string str, Encoding? encoding = null)
    {
        var enc = encoding ?? DefaultEncoding;
        return enc.GetString(Convert.FromBase64String(str));
    }
}