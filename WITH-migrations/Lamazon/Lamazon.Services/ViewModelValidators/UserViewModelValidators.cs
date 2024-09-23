using FluentValidation;
using Lamazon.Domain.Constants;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;

namespace Lamazon.Services.ViewModelValidators
{
    public class UserCredentialsViewModelValidator : AbstractValidator<UserCredentialsViewModel>
    {
        public UserCredentialsViewModelValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required")
               .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }

    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator(IUsersService usersService)
        {
            RuleFor(x => x)
                .Must(x => usersService.HasOtherAdmins(x))
                .When(x => x.RoleKey != Roles.Admin).WithName(x => nameof(x.RoleKey))
                .WithMessage("There must be at least one admin on the platform.");

            RuleFor(x => x.Email)
              .NotEmpty().WithMessage("Email is required")
              .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }

    public class RegisterUserViewModelValidator : AbstractValidator<RegisterUserViewModel>
    {
        public RegisterUserViewModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(255);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .MaximumLength(200);

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name required")
                .MaximumLength(500).WithMessage("Full Name must have maximum 500 characters long");

        }
    }
}
