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
        CreateMap<Book, BookDto>()
            .ForMember(
                dest => dest.Author,
                opt => opt.
                    MapFrom(src => src.Authors)).ReverseMap();
        CreateMap<Book, BookDto>()
            .ForMember(
                dest => dest.Genres,
                opt => opt.
                    MapFrom(src => src.Genres)).ReverseMap();
        /*CreateMap<BookLoan, BookLoanDto>()
            .ForMember(
                dest => dest.Book,
                opt => opt.
                    MapFrom(src => src.Book)).ReverseMap();*/
        /*CreateMap<Book, BookDto>()
            .ForMember(
                dest=> dest.BookLoans,
                opt=> opt
                    .MapFrom(src=> src.BookLoans)).ReverseMap();*/
        CreateMap<UserLoanDto, User>().ReverseMap();
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
        
        CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();
        

        CreateMap<IEnumerable<User>, IEnumerable<UserDto>>();




    }
    
}