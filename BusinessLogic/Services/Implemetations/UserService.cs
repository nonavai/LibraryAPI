using AutoMapper;
using BusinessLogic.Models.BookLoan;
using BusinessLogic.Models.User;
using BusinessLogic.Validators;
using DataLayer.Entities;
using DataLayer.Repositories;
using FluentValidation;
using Shared.Exceptions;
using ValidationException = Shared.Exceptions.ValidationException;


namespace BusinessLogic.Services.Implemetations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UserDto> _validator;

    public UserService(IUserRepository userRepository, IMapper mapper, IValidator<UserDto> validator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var entity = await _userRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new NotFoundException("Car not found");
        }
        var dto = _mapper.Map<UserDto>(entity);
        return dto;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var userDto = _mapper.Map<IEnumerable<UserDto>>( await _userRepository.GetAllAsync());
        return userDto;
    }

    public async Task<UserDto> AddAsync(UserDto model)
    {
        if (await IsEmailExist(model.Email))
        {
            throw new BadAuthorizeException("User already exist");
        }
        var validationResult = _validator.Validate(model);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }
        
        // CREATING USER ROLES
        var entity = _mapper.Map<User>(model);
        var compitedUser = await _userRepository.AddAsync(entity);
        var dto = _mapper.Map<UserDto>(  compitedUser);
        return dto;
    }

    public async Task<UserDto> UpdateAsync(UserDto model)
    {
        var existingEntity = await _userRepository.GetByIdAsync(model.Id);
        if (existingEntity == null)
        {
            throw new NotFoundException("Book not found");
        }
        //check on existing
        var validationResult = _validator.Validate(model);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }

        existingEntity.FirstName = model.FirstName;
        existingEntity.LastName = model.LastName;
        existingEntity.Password = model.Password;
        existingEntity.PhoneNumber = model.PhoneNumber;
        existingEntity.Description = model.Description;


        var dto = _mapper.Map<UserDto>(await _userRepository.UpdateAsync(existingEntity));
        return dto;
    }

    public async Task<UserDto> DeleteAsync(int id)
    {
        var entity = await _userRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new NotFoundException("User not found");
        }
        
        var dto = _mapper.Map<UserDto>( await _userRepository.DeleteAsync(id));
        return dto;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _userRepository.ExistsAsync(id);
    }

    public async Task<UserDto> GetByEmailAsync(string email)
    {
        var entity = await _userRepository.GetByEmailAsync(email);
        if (entity == null)
        {
            throw new NotFoundException("Email Not Found");
        }
        var dto = _mapper.Map<UserDto>(entity);
        return dto;
    }

    public async Task<bool> IsEmailExist(string email)
    {
        var entity = await _userRepository.GetByEmailAsync(email);
        return entity != null;
    }

    public async Task<UserLoanDto> GetWithLoans(int id)
    {
        var entity = await _userRepository.GetWithLoans(id);
        if (entity == null)
        {
            throw new NotFoundException("User not found");
        }

        var dto = _mapper.Map<UserLoanDto>(entity);
        return dto;
    }

    public async Task<IQueryable<BookLoanDto>> GetLoansByUser(int id)
    {
        var dto = _mapper.Map<IQueryable<BookLoanDto>>( await _userRepository.GetLoansByUser(id));
        return dto;
    }
}