using AutoMapper;
using BookLendingAPI.Dtos;
using BookLendingAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookLendingAPI.Mapping
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookUpdateDto, Book>().ReverseMap();
        }
    }
}
