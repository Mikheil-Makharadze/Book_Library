using API.DTO;
using AutoMapper;
using Core.Entities;

namespace API.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public AutoMapperProfile()
        {
            //Author
            CreateMap<Author, AuthorDTO>()
                .ForPath(ad => ad.Books, m => m.MapFrom(a => a.AuthorBooks.Select(n => n.Book)))
                .ReverseMap();

            CreateMap<Author, AuthorCreateDTO>().ReverseMap();

            CreateMap<Author, SelectorItems>()
                .ForPath(si => si.Id, m => m.MapFrom(a => a.Id))
                .ForPath(si => si.Name, m => m.MapFrom(a => a.Name + " " + a.Surname));


            //Book
            CreateMap<Book, BookDTO>()
                .ForPath(bd => bd.Authors, m => m.MapFrom(b => b.AuthorBooks.Select(n => n.Author)))
                .ReverseMap();

            CreateMap<BookCreateDTO, Book>().ReverseMap();

            CreateMap<Book, SelectorItems>()
                .ForPath(si => si.Id, m => m.MapFrom(b => b.Id))
                .ForPath(si => si.Name, m => m.MapFrom(b => b.Title));
        }
    }
}
