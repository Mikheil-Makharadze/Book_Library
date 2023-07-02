using API.DTO;
using AutoMapper;
using Core.Entities;

namespace API.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Author
            CreateMap<Author, AuthorDTO>()
                .ForPath(ad => ad.books, m => m.MapFrom(a => a.AuthorBooks.Select(n => n.Book)))
                .ReverseMap();

            CreateMap<Author, AuthorCreateDTO>().ReverseMap();


            //Book
            CreateMap<Book, BookDTO>()
                .ForPath(bd => bd.Authors, m => m.MapFrom(b => b.AuthorBooks.Select(n => n.Author)))
                .ReverseMap();

            CreateMap<BookCreateDTO, Book>().ReverseMap();
        }
    }
}
