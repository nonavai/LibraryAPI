using AutoMapper;
using BusinessLogic.Models.Author;
using BusinessLogic.Models.Book;
using BusinessLogic.Models.BookLoan;
using BusinessLogic.Models.Genre;
using BusinessLogic.Models.RefreshToken;
using BusinessLogic.Models.User;
using BusinessLogic.Services.Implemetations;
using DataAccess.Entities;
using DataLayer.Entities;

namespace BusinessLogic.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        /*CreateMap<Author, AuthorDto>();
        CreateMap<Genre, GenreDto>();*/
        CreateMap<Book, BookDto>()
            .ForMember(
                dest => dest.Genres,
                opt => opt.
                    MapFrom(src => src.Genres))
            .ForMember(
                dest => dest.Authors,
                opt => opt.
                    MapFrom(src => src.Authors))
            .ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<User, UserLoanDto>()
            .ForMember(
                dest=> dest.BookLoans,
                opt=> opt
                    .MapFrom(src=> src.BookLoans)).ReverseMap();
        CreateMap<Author, AuthorDto>()
            .ForMember(
                dest => dest.Books,
                opt => opt.
                    MapFrom(src => src.Books)).ReverseMap();
        CreateMap<Genre, GenreDto>()
            .ForMember(
                dest => dest.Books,
                opt => opt.
                    MapFrom(src => src.Books)).ReverseMap();
        CreateMap<BookLoan, BookLoanDto>().ReverseMap();
        CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();
        

        
        
        CreateMap<IEnumerable<User>, IEnumerable<UserDto>>().ReverseMap();
        CreateMap<IQueryable<BookLoan>, IQueryable<BookLoanDto>>().ReverseMap();
        CreateMap<IEnumerable<Author>, IEnumerable<AuthorDto>>().ReverseMap();
        CreateMap<IEnumerable<BookLoan>, IEnumerable<BookLoanDto>>().ReverseMap();
        CreateMap<IEnumerable<Genre>, IEnumerable<GenreDto>>().ReverseMap();
        CreateMap<IQueryable<Genre>, IQueryable<GenreDto>>().ReverseMap();





    }
    
}