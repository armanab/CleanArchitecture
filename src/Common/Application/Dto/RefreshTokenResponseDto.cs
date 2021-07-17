using CleanApplication.Domain.Entities;
using System;
using System.Text.Json.Serialization;

namespace CleanApplication.Application.Dto
{
    public class RefreshTokenResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }

        public RefreshTokenResponseDto(ApplicationUser user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    } 
    }
