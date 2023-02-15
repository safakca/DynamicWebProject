using System;
using AutoMapper;
using DtoLayer.Concrete;
using EntityLayer.Concrete;

namespace BusinessLayer.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Product, ProductDto>().ReverseMap();
	}
}

