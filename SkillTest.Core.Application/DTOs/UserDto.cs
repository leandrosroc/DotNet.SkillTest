using System.Text.Json.Serialization;

namespace SkillTest.Core.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public byte[]? PasswordHash { get; set; }
        [JsonIgnore]
        public byte[]? PasswordSalt { get; set; }

        public string Role { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; } = string.Empty;
        [JsonIgnore]
        public DateTime? TokenCreated { get; set; }
        [JsonIgnore]
        public DateTime? TokenExpires { get; set; }
    }
}
