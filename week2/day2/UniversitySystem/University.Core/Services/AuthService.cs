namespace University.Core.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Core.DTOs;
using University.Core.Exceptions;
using University.Core.Forms;
using University.Core.Validation;
using University.Data.Entities;

public interface IAuthService
{
    Task<UserDto> Register(RegisterForm form);
    Task<UserDto> Login(LoginForm form);
}

public class AuthService(
    SignInManager<User> signInManager,
    UserManager<User> userManager,
    RoleManager<Role> roleManager,
    ILogger<AuthService> logger) : IAuthService
{
    public async Task<UserDto> Register(RegisterForm form)
    {
        FormValidator.Validate(form);

        var existingUser = await userManager.FindByEmailAsync(form.Email);
        if (existingUser != null)
        {
            throw new BusinessException(new Dictionary<string, List<string>> { { "Email", new List<string> { "User with this email already exists." } } });
        }

        var roleExists = await roleManager.RoleExistsAsync(form.Role);
        if (!roleExists)
        {
            throw new BusinessException(new Dictionary<string, List<string>> { { "Role", new List<string> { $"Role '{form.Role}' is invalid." } } });
        }

        var user = new User
        {
            UserName = form.Email,
            Email = form.Email,
            FirstName = form.FirstName,
            LastName = form.LastName
        };

        var result = await userManager.CreateAsync(user, form.Password);
        if (!result.Succeeded)
        {
            var errors = new Dictionary<string, List<string>>();
            foreach (var error in result.Errors)
            {
                if (!errors.ContainsKey("Password")) errors["Password"] = new List<string>();
                errors["Password"].Add(error.Description);
            }
            throw new BusinessException(errors);
        }

        await userManager.AddToRoleAsync(user, form.Role);

        logger.LogInformation("User {Email} registered successfully with role {Role}", user.Email, form.Role);

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            Phone = user.PhoneNumber ?? string.Empty,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            Role = form.Role
        };
    }

    public async Task<UserDto> Login(LoginForm form)
    {
        FormValidator.Validate(form);

        var result = await signInManager.PasswordSignInAsync(form.Email, form.Password, form.RememberMe, lockoutOnFailure: true);

        if (result.IsLockedOut)
            throw new BusinessException(new Dictionary<string, List<string>> { { "Login", new List<string> { "Account is locked out due to multiple failed attempts." } } });

        if (result.IsNotAllowed)
            throw new BusinessException(new Dictionary<string, List<string>> { { "Login", new List<string> { "Account is not allowed to sign in." } } });

        if (!result.Succeeded)
            throw new BusinessException(new Dictionary<string, List<string>> { { "Login", new List<string> { "Invalid email or password." } } });

        var user = await userManager.FindByEmailAsync(form.Email);
        var roles = await userManager.GetRolesAsync(user!);

        logger.LogInformation("User {Email} logged in successfully.", user!.Email);

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            EmailConfirmed = user.EmailConfirmed,
            Phone = user.PhoneNumber ?? string.Empty,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            Role = roles.FirstOrDefault() ?? "Student"
        };
    }
}