using SIADG.Toolkit.Text;
using System.Diagnostics.CodeAnalysis;

namespace SIADG.Api.Security;

public static class ApiKeyEncoding
{
    public static bool TryDecode(
        string encodedValue,
        [MaybeNullWhen(false)] out string valorSecreto
    )
    {
        valorSecreto = null;

        try
        {
            valorSecreto = encodedValue.DecodeBase64();
            return true;
        }
        catch (Exception exception)
        {
            throw new InvalidApiKeyFormatException(exception);
        }
    }
}
