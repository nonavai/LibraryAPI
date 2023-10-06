using AutoMapper;
using BusinessLogic.Models.Author;
using BusinessLogic.Models.Book;
using BusinessLogic.Models.BookLoan;
using BusinessLogic.Models.Genre;
using BusinessLogic.Models.User;
using LibraryAPI.Requests.Author;
using LibraryAPI.Requests.Book;
using LibraryAPI.Requests.BookLoan;
using LibraryAPI.Requests.Genre;
using LibraryAPI.Requests.User;
using LibraryAPI.Responses.Author;
using LibraryAPI.Responses.Book;
using LibraryAPI.Responses.BookLoan;
using LibraryAPI.Responses.Genre;
using LibraryAPI.Responses.User;

namespace LibraryAPI.Mapping;

public class MappingProfileApi : Profile
{
    public MappingProfileApi()
    {
        CreateMap<UserRequest, UserDto>();
        CreateMap<CreateUserRequest, UserDto>();
        CreateMap<LogInRequest, UserDto>();
        CreateMap<AuthorRequest, AuthorDto>();
        CreateMap<GenreRequest, GenreDto>();
        CreateMap<BookLoanRequest, BookLoanDto>();
        CreateMap<BookRequest, BookDto>();
        CreateMap<AddBookRelations, RelationsDto>();



        CreateMap<UserDto, LogInResponse>();
        CreateMap<UserDto, UserResponse>();
        CreateMap<BookLoanDto, BookLoanResponse>();
        CreateMap<UserLoanDto, UserLoanResponse>()
            .ForMember(dest => dest.BookLoans, opt => opt.MapFrom(src => src.BookLoans.Select(c => new BookLoanResponse()
            {
                Id = c.Id,
                UserId = c.UserId,
                BookId = c.BookId,
                LoanDate = c.LoanDate,
                ReturnDate = c.ReturnDate
            })));
        CreateMap<AuthorDto, AuthorBooksResponse>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(c => new BookClearResponse
            {
                Id = c.Id,
                Title = c.Title,
                ISBN13 = c.ISBN13,
                Name = c.Name,
                Description = c.Description,
                IsAvailable = c.IsAvailable
            })));
        CreateMap<AuthorDto, AuthorResponse>();
        CreateMap<GenreDto, GenreBooksResponse>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(c => new BookClearResponse
            {
                Id = c.Id,
                Title = c.Title,
                ISBN13 = c.ISBN13,
                Name = c.Name,
                Description = c.Description,
                IsAvailable = c.IsAvailable
            })));
        CreateMap<GenreDto, GenreResponse>();
        CreateMap<BookDto, BookResponse>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(c => new GenreResponse
            {
                Id = c.Id,
                Name = c.Name
            })))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.Select(c => new AuthorResponse
            {
                Id = c.Id,
                Name = c.Name,
                Birth = c.Birth,
                Death = c.Death,
                Description = c.Description
            })));
        CreateMap<BookClearDto, BookClearResponse>();
    }
}