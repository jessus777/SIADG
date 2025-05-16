using System.Collections.Concurrent;
using Timer = System.Timers.Timer;
namespace SIADG.Api.Security;
using AspNetCore.Authentication.ApiKey;
public static class ApiKeyAuthenticationFlow
{
    public const string HeaderName = "X-API-KEY";
    //private static readonly ConcurrentDictionary<string, CuentaServicioDto> Cache = new();
    private static readonly ConcurrentDictionary<string, Object> Cache = new();

    private static readonly Timer Timer = new();

    static ApiKeyAuthenticationFlow()
    {
        Timer.Interval = TimeSpan.FromMinutes(15).TotalMilliseconds;
        Timer.Elapsed += (_, _) => Cache.Clear();
        Timer.Start();
    }

    public static async Task OnValidateKey(ApiKeyValidateKeyContext context)
    {
        try
        {
            var apiKey = context.ApiKey;
            if (apiKey is null)
            {
                context.ValidationFailed("NoSeProporcionoApiKey");
                return;
            }

            if (!Cache.TryGetValue(apiKey, out var cuentaServicio))
            {
                if (!ApiKeyEncoding.TryDecode(apiKey, out var valorSecreto))
                {
                    context.ValidationFailed("ApiKeyNoValido");
                    return;
                }

                //var unitOfWorkFactory = context.HttpContext.RequestServices.GetRequiredService<IUnitOfWorkFactory>();
                //await using var unitOfWork = unitOfWorkFactory.Create<ISeguridadUnitOfWork>();

                //cuentaServicio = await unitOfWork.CuentaServicioRepository.ValidarCuentaServicioAsync(
                //    valorSecreto,
                //    CancellationToken.None
                //);
            }

            if (cuentaServicio is null)
            {
                context.ValidationFailed("ApiKeyNoValido");
                return;
            }

            Cache[apiKey] = cuentaServicio;

            context.ValidationSucceeded(
            //cuentaServicio.NombreCorto,
            //[
            //    //new Claim(ExtendedClaimTypes.IdCuentaServicio, cuentaServicio.IdCuentaServicio),
            //    //new Claim(ExtendedClaimTypes.CodigoCuentaServicio, cuentaServicio.Codigo),
            //    //new Claim(ExtendedClaimTypes.NombreCortoCuentaServicio, cuentaServicio.NombreCorto),
            //    //new Claim(ExtendedClaimTypes.NombreLargoCuentaServicio, cuentaServicio.NombreLargo)
            //]
            );
        }
        catch (Exception exception)
        {
            var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(nameof(ApiKeyAuthenticationFlow));
            logger.LogError(exception, "Could not validate API Key");
            context.ValidationFailed(exception);
        }
    }
}
