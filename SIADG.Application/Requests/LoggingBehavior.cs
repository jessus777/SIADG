using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Globalization;

namespace SIADG.Application.Requests;
public sealed class LoggingBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResult>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResult>> logger)
    {
        _logger = logger;
    }

    public async Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        var requestName = request.GetType().Name;
        var cultureName = CultureInfo.CurrentUICulture.Name;

        _logger.LogInformation(
            "Executing request {RequestName} ({Culture})\n{@Request}",
            requestName,
            cultureName,
            request
            );

        var stopwatch = new Stopwatch();
        try
        {
            stopwatch.Start();
            var result = await next();
            stopwatch.Stop();

            var resultType = result?.GetType();

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug(
                    "Request {RequestName} ({Culture}) executed in {ElapsedTime:N} ms with result of type {ResultType} [ {@Result} ]",
                    requestName,
                    cultureName,
                    stopwatch.ElapsedMilliseconds,
                    resultType?.Name,
                    result
                );
            }
            else
            {
                _logger.LogInformation(
                    "Request {RequestName} ({Culture}) executed in {ElapsedTime:N} ms with result of type {ResultType}",
                    requestName,
                    cultureName,
                    stopwatch.ElapsedMilliseconds,
                    resultType?.Name
                );
            }

            return result;
        }
        catch (Exception)
        {
            if (stopwatch.IsRunning)
                stopwatch.Stop();

            throw;
        }
    }
}
