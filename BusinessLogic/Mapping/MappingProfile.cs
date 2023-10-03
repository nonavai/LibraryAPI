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
using Shared.Enums;

namespace BusinessLogic.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<IEnumerable<User>, IEnumerable<UserDto>>().ReverseMap();
        CreateMap<IQueryable<BookLoan>, IQueryable<BookLoanDto>>().ReverseMap();
        CreateMap<IEnumerable<Author>, IEnumerable<AuthorDto>>().ReverseMap();
        CreateMap<IEnumerable<Genre>, IEnumerable<GenreDto>>().ReverseMap();
        CreateMap<IQueryable<Genre>, IQueryable<GenreDto>>().ReverseMap();
        
        CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();
        CreateMap<Book, BookDto>()
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
        CreateMap<BookLoan, BookLoanDto>().ReverseMap();
        CreateMap<IEnumerable<BookLoan>, IEnumerable<BookLoanDto>>().ReverseMap();
        CreateMap<User, UserLoanDto>()
            .ForMember(dest => dest.BookLoans, opt => opt.MapFrom(src => src.BookLoans.Select(c => new BookLoanDto
            {
                Id = c.Id,
                UserId = c.UserId,
                BookId = c.BookId,
                LoanDate = c.LoanDate,
                ReturnDate = c.ReturnDate
            }))).ReverseMap();
        CreateMap<Author, AuthorDto>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(c => new BookClearDto
            {
                Id = c.Id,
                Title = c.Title,
                ISBN13 = c.ISBN13,
                Name = c.Name,
                Description = c.Description,
                IsAvailable = c.IsAvailable
            }))).ReverseMap();
        CreateMap<Genre, GenreDto>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(c => new BookClearDto
            {
                Id = c.Id,
                Title = c.Title,
                ISBN13 = c.ISBN13,
                Name = c.Name,
                Description = c.Description,
                IsAvailable = c.IsAvailable
            }))).ReverseMap();
        

    }
    
}