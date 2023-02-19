using System;
using System.Reflection;
using BusinessLayer.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer;

public static class BusinessServiceRegistration
{
	public static void AddBusinessServices(this IServiceCollection services)
	{
		services.AddAutoMapper(typeof(MappingProfile));
		services.AddMediatR(Assembly.GetExecutingAssembly());

    }
}

