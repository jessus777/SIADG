using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SIADG.Application.Requests;

namespace SIADG.Application.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add application services here
        // Example: services.AddTransient<IMyService, MyService>();
        services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(config => {
            //services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(OperationContextBehavior<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        return services;
    }
}
