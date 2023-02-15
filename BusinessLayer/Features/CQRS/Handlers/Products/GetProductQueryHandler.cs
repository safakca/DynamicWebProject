using System;
using AutoMapper;
using BusinessLayer.Repositories;
using DtoLayer.Concrete;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Queries;

public class GetProductsQueryHandler : IRequestHandler<GetProductQueryRequest, List<ProductDto>>
{
    private readonly IRepository<Product> _repository;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IRepository<Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAllAsync();
        return _mapper.Map<List<ProductDto>>(data);
    }
}

