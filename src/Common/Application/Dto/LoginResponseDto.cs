using System;

namespace CleanApplication.Application.Dto
{
    public class LoginResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expire { get; set; }
    }
}
