﻿using AutoMapper;
using BusinessLogic.Models.BookLoan;
using BusinessLogic.Models.User;
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

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
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

    public async Task<IEnumerable<UserSecureDto>> GetAllAsync()
    {
        var userDto = _mapper.Map<IEnumerable<UserSecureDto>>( await _userRepository.GetAllAsync());
        return userDto;
    }

    public async Task<UserDto> AddAsync(UserClearDto model)
    {
        if (await IsEmailExist(model.Email))
        {
            throw new BadAuthorizeException("User already exist");
        }

        // CREATING USER ROLES
        var entity = _mapper.Map<User>(model);
        var compitedUser = await _userRepository.AddAsync(entity);
        var dto = _mapper.Map<UserDto>(  compitedUser);
        return dto;
    }

    public async Task<UserDto> UpdateAsync(int id, UserClearDto model)
    {
        var existingEntity = await _userRepository.GetByIdAsync(id);
        if (existingEntity == null)
        {
            throw new NotFoundException("Book not found");
        }
        //check on existing

        _mapper.Map(model, existingEntity);

        var dto = _mapper.Map<UserDto>(await _userRepository.UpdateAsync(existingEntity));
        return dto;
    }

    public async Task<UserSecureDto> DeleteAsync(int id)
    {
        var entity = await _userRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new NotFoundException("User not found");
        }
        
        var dto = _mapper.Map<UserSecureDto>( await _userRepository.DeleteAsync(id));
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
        var entities = await _userRepository.GetLoansByUser(id);
        var dto = entities.Select(entity=> _mapper.Map<BookLoanDto>(entity));
        return dto;
    }

    public async Task<UserDto> LogInAsync(string email, string password)
    {
        var dto = await GetByEmailAsync(email);
        if (dto.Password != password)
        {
            throw new BadAuthorizeException("Wrong Password");
        }

        return dto;
    }
}