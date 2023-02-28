using BusinessLayer.Mappings;
using BusinessLayer.Validations;
using DtoLayer.Concrete.Articles;
using DtoLayer.Concrete.Authors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BusinessLayer;

public static class BusinessServiceRegistration
{
    public static void AddBusinessServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddTransient<IValidator<CreateAuthorDto>, AuthorAddValidator>();
        services.AddTransient<IValidator<CreateArticleDto>, ArticleAddValidator>();

    } 
}

