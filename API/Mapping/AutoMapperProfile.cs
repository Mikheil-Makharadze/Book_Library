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


            //Book
            CreateMap<Book, BookDTO>()
                .ForPath(bd => bd.Authors, m => m.MapFrom(b => b.AuthorBooks.Select(n => n.Author)))
                .ReverseMap();

            CreateMap<BookCreateDTO, Book>().ReverseMap();
        }
    }
}
