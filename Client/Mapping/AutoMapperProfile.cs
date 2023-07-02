using AutoMapper;
using Client.Models.DTO;
using Clinet.Models.DTO;

namespace Client.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<BookDTO, BookUpdateDTO>()
                .ForPath(cd => cd.AuthorsId, m => m.MapFrom(n => n.Authors.Select(n => n.Id)))
                .ReverseMap();

            CreateMap<AuthorDTO, AuthorUpdateDTO>()
                .ForPath(cd => cd.booksId, m => m.MapFrom(c => c.books.Select(n => n.Id)))
                .ReverseMap();

        }
    }
}
