using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimewaysAPI.Application.Auth;
using TimewaysAPI.Domain.Entities;
using TimewaysAPI.Infrastructure.Persistence;

namespace TimewaysAPI.Infrastructure.Auth;

public sealed class AuthService : IAuthService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly JwtTokenService _jwtTokenService;

    public AuthService(
        ApplicationDbContext dbContext,
        PasswordHasher<User> passwordHasher,
        JwtTokenService jwtTokenService)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResponse> RegisterAsync(
        RegisterRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var emailExists = await _dbContext.Users
            .AnyAsync(x => x.Email == email);

        if (emailExists)
        {
            throw new InvalidOperationException(
                "A user with this email already exists.");
        }

        var user = new User
        {
            Name = request.Name.Trim(),
            Email = email
        };

        user.PasswordHash = _passwordHasher.HashPassword(
            user,
            request.Password);

        _dbContext.Users.Add(user);

        await _dbContext.SaveChangesAsync();

        return new AuthResponse
        {
            AccessToken = _jwtTokenService.GenerateToken(user)
        };
    }

    public async Task<AuthResponse?> LoginAsync(
        LoginRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var user = await _dbContext.Users
            .SingleOrDefaultAsync(x => x.Email == email);

        if (user is null)
        {
            return null;
        }

        var passwordResult =
            _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password);

        if (passwordResult ==
            PasswordVerificationResult.Failed)
        {
            return null;
        }

        return new AuthResponse
        {
            AccessToken = _jwtTokenService.GenerateToken(user)
        };
    }
}