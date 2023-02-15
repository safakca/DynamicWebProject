using System;
using BusinessLayer.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer;

public static class BusinessServiceRegistration
{
	public static void AddBusinessServices(this IServiceCollection services)
	{
		services.AddAutoMapper(typeof(MappingProfile));
	}
}

