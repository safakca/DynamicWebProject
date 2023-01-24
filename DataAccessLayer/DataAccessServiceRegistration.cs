using System;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer;

public static class DataAccessServiceRegistration
{
	public static void AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<Context>(opt =>
		{
			opt.UseNpgsql(configuration.GetConnectionString("Local"));
		});
        services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();

    }
}

