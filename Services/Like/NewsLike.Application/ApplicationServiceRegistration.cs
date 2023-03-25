using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using NewsLike.Application.Features.Likes.Commands.CreateLike;
using NewsLike.Application.Features.Likes.Commands.DeleteLike;

namespace NewsLike.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IValidator<CreateLikeCommand>, CreateLikeCommandValidator>();
            services.AddTransient<IValidator<DeleteLikeCommand>, DeleteLikeCommandValidator>();


            return services;
        }
    }
}
