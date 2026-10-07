namespace MyWorkout.Application.DTOs
{
    public class AccountDto
    {
        private string _email = string.Empty;

        public string Username { get; set; } = string.Empty;
        public string Email
        {
            get => _email;
            set => _email = (value ?? string.Empty).Trim();
        }
        public string Password { get; set; } = string.Empty;
    }

    public class LoginDto
    {
        private string _email = string.Empty;

        public string Email
        {
            get => _email;
            set => _email = (value ?? string.Empty).Trim();
        }
        public string Password { get; set; } = string.Empty;
    }

    public class AccountResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime AccessTokenExpiresAtUtc { get; set; }
    }



}
