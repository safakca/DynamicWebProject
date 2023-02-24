using AutoMapper;
using BusinessLayer.Features.CQRS.Queries.Products;
using BusinessLayer.Repositories;
using DtoLayer.Concrete;
using EntityLayer.Concrete;
using MediatR;

namespace BusinessLayer.Features.CQRS.Handlers.Products;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, ProductDto>
{
    private readonly IRepository<Product> _repository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IRepository<Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetByFilterAsync(x => x.Id == request.Id);
        return _mapper.Map<ProductDto>(data);
    }
}

