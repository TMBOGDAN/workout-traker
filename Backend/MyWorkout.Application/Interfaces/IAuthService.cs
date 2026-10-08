using MyWorkout.Application.DTOs;

namespace MyWorkout.Application.Interfaces;

public interface IAuthService
{
    Task<AccountResponseDto?> LoginAsync(
        LoginDto loginDto,
        CancellationToken cancellationToken = default);

    Task<bool> RegisterAsync(
        AccountDto accountDto,
        CancellationToken cancellationToken = default);
}
