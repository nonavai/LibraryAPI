using AutoMapper;
using BusinessLogic.Models.Author;
using BusinessLogic.Models.Book;
using BusinessLogic.Models.BookLoan;
using BusinessLogic.Models.Genre;
using BusinessLogic.Models.User;
using LibraryAPI.Requests.Author;
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
        
        
        
        CreateMap<UserDto, LogInResponse>();
        CreateMap<UserDto, UserResponse>();
        CreateMap<UserLoanDto, UserLoanResponse>()
            .ForMember(
            dest=> dest.BookLoans,
            opt=> opt
                .MapFrom(src=> src.BookLoans)).ReverseMap();;
        CreateMap<AuthorDto, AuthorBooksResponse>()
            .ForMember(
                dest => dest.Books,
                opt => opt.
                    MapFrom(src => src.Books));
        CreateMap<AuthorDto, AuthorResponse>();
        CreateMap<GenreDto, GenreBooksResponse>()
            .ForMember(
                dest => dest.Books,
                opt => opt.
                    MapFrom(src => src.Books));
        CreateMap<GenreDto, GenreResponse>();
        CreateMap<BookLoanDto, BookLoanResponse>();
        CreateMap<BookDto, BookResponse>()
            .ForMember(
                dest => dest.Genres,
                opt => opt.
                    MapFrom(src => src.Genres))
            .ForMember(
                dest => dest.Authors,
                opt => opt.
                    MapFrom(src => src.Authors))
            .ReverseMap();;
        CreateMap<BookClearDto, BookClearResponse>();

        CreateMap<IEnumerable<UserDto>, IEnumerable<UserResponse>>();
        CreateMap<IEnumerable<AuthorDto>, IEnumerable<AuthorResponse>>();
        CreateMap<IEnumerable<BookDto>, IEnumerable<BookResponse>>();
        CreateMap<IEnumerable<GenreDto>, IEnumerable<GenreResponse>>();
        CreateMap<IQueryable<BookDto>, IQueryable<BookResponse>>();
        CreateMap<IQueryable<BookLoanDto>, IQueryable<BookLoanResponse>>();


    }
}