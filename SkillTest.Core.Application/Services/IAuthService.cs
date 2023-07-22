using SkillTest.Core.Application.Auth;
using SkillTest.Core.Application.DTOs;

namespace SkillTest.Core.Application.Services
{
    public interface IAuthService
    {
        public string CreateToken(UserDto user, string myRole);
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        public RefreshToken GenerateRefreshToken();
    }
}
