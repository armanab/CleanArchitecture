namespace CleanApplication.Application.Dto
{
    public class RevokeTokenResponseDto
    {
     
        public string Message { get; set; }
        public bool IsRevoked { get; set; }

        public RevokeTokenResponseDto(bool isRevoked, string message)
        {
            IsRevoked= isRevoked;
            Message = message;
        }
    } 
    }
