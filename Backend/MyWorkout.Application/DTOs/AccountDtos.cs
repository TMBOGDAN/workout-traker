namespace AccountDtos
{
    public class AccountDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }

    public class AccountResponseDto
    {
        public string Token { get; set; } = null!;

    }


}