using AutoMapper;
using BookLendingAPI.Dtos;
using BookLendingAPI.Models;

namespace BookLendingAPI.Mapping
{
    public class BorrowingProfile : Profile
    {
        public BorrowingProfile()
        {
            CreateMap<Borrowing, BorrowingDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
        }
    }

}
