using SIADG.Api.Security;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;

namespace SIADG.Api.Extensions;

public static class HttpExtensions
{
    public static string? GetHeaderValue(this HttpContext context, string headerName)
        => context.Request.Headers.TryGetValue(headerName, out var values)
            ? values.FirstOrDefault()
            : null;

    public static string? GetApiKey(this HttpContext context)
        => context.GetHeaderValue(ApiKeyAuthenticationFlow.HeaderName);

    public static CultureInfo GetPreferredCulture(this HttpContext context)
    {
        var preferredLanguage = context.Request.Headers.AcceptLanguage.FirstOrDefault();
        var cultureName = StringWithQualityHeaderValue.TryParse(preferredLanguage, out var parsedValue)
            ? parsedValue.Value
            : null;

        return cultureName is not null ? new CultureInfo(cultureName) : CultureInfo.CurrentUICulture;
    }

    public static async Task WriteAsync(this HttpResponse response, MultipartContent multipartContent, CancellationToken cancellationToken)
    {
        var boundary = multipartContent.Headers.ContentType!.Parameters.First(p => p.Name == "boundary").Value;
        response.ContentType = "multipart/mixed; boundary=" + boundary;

        var writer = response.BodyWriter;
        foreach (var content in multipartContent)
        {
            await writer.WriteAsync(Encoding.UTF8.GetBytes(content.Headers.ToString()), cancellationToken);
            await content.CopyToAsync(writer.AsStream(), cancellationToken);
            await writer.WriteAsync("\r\n"u8.ToArray(), cancellationToken);
        }
        await writer.FlushAsync(cancellationToken);
    }
}
