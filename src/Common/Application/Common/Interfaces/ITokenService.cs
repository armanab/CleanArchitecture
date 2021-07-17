using System;

namespace CleanApplication.Application.Common.Interfaces
{
    public interface ITokenService
    {
        string CreateJwtSecurityToken(string id,string key, DateTime expireDateTime);
    }
}
