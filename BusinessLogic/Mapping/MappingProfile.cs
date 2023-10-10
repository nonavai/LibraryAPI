using AutoMapper;
using BusinessLogic.Models.Author;
using BusinessLogic.Models.Book;
using BusinessLogic.Models.BookLoan;
using BusinessLogic.Models.Genre;
using BusinessLogic.Models.RefreshToken;
using BusinessLogic.Models.User;
using DataAccess.Entities;
using DataLayer.Entities;

namespace BusinessLogic.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserClearDto>().ReverseMap();
        CreateMap<Author, AuthorClearDto>().ReverseMap();
        CreateMap<Genre, GenreClearDto>().ReverseMap();
        CreateMap<Book, BookClearDto>().ReverseMap();
        CreateMap<BookLoan, BookLoanDto>().ReverseMap();
        CreateMap<BookLoan, BookLoanClearDto>().ReverseMap();
        CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();
        CreateMap<Book, BookFullDto>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(c => new GenreDto
            {
                Id = c.Id,
                Name = c.Name
            })))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.Select(c => new AuthorDto
            {
                Id = c.Id,
                Name = c.Name,
                Birth = c.Birth,
                Death = c.Death,
                Description = c.Description
            }))).ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<User, UserSecureDto>().ReverseMap();
        CreateMap<User, UserLoanDto>()
            .ForMember(dest => dest.BookLoans, opt => opt.MapFrom(src => src.BookLoans.Select(c => new BookLoanDto
            {
                Id = c.Id,
                UserId = c.UserId,
                BookId = c.BookId,
                LoanDate = c.LoanDate,
                ReturnDate = c.ReturnDate
            }))).ReverseMap();
        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorBookDto>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(c => new BookDto
            {
                Id = c.Id,
                Title = c.Title,
                ISBN13 = c.ISBN13,
                Name = c.Name,
                Description = c.Description,
                IsAvailable = c.IsAvailable
            }))).ReverseMap();
        CreateMap<Genre, GenreDto>();
        CreateMap<Genre, GenreBookDto>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(c => new BookDto
            {
                Id = c.Id,
                Title = c.Title,
                ISBN13 = c.ISBN13,
                Name = c.Name,
                Description = c.Description,
                IsAvailable = c.IsAvailable
            }))).ReverseMap();


        //CreateMap<IEnumerable<Book>, IEnumerable<BookDto>>();




    }
    
}