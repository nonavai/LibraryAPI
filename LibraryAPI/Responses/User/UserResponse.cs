﻿using Shared.Enums;

namespace LibraryAPI.Responses.User;

public record UserResponse
{
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string? Description { get; set; }
}