using Application.Behaviors;
using Application.Routes.Commands.CreateRoute;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateRouteCommandValidator>());

            return services;
        }
    }
}