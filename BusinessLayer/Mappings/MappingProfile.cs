using AutoMapper;
using BusinessLayer.Features.CQRS.Commands.Articles;
using BusinessLayer.Features.CQRS.Commands.Authors;
using DtoLayer.Concrete.Articles;
using DtoLayer.Concrete.Authors;
using EntityLayer.Concrete;

namespace BusinessLayer.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        #region Author
        CreateMap<Author, AuthorsDto>().ReverseMap();
        CreateMap<CreateAuthorCommandRequest, Author>().ReverseMap();
        CreateMap<UpdateAuthorCommandRequest, Author>().ReverseMap();
        #endregion 

        #region Article
        CreateMap<Article, ArticlesDto>().ReverseMap();
        CreateMap<CreateArticleCommandRequest, Article>().ReverseMap(); 
        CreateMap<UpdateArticleCommandRequest, Article>().ReverseMap();

        #endregion
    }
}

