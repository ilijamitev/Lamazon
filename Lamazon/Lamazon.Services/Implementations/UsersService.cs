using AutoMapper;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Constants;
using Lamazon.Domain.Enitities;
using Lamazon.Services;
using Lamazon.Services.Helpers;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using System;
using System.Collections.Generic;

namespace Lamazon.Services.Implementations
{
    public class UsersService : BaseService, IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public UserViewModel RegisterUser(RegisterUserViewModel userViewModel)
        {
            LogInfo($"Processing user registration... Email: {userViewModel.Email}");
            var user = _mapper.Map<User>(userViewModel);
            PasswordHashedHelper.HashPassword(user, userViewModel.Password);
            user.RoleKey = Roles.User;
            var userId = _usersRepository.Insert(user);
            if (userId <= 0)
            {
                throw new Exception("User not saved.");
            }
            LogInfo($"Successfully registered new user. Email: {userViewModel.Email}");
            return GetUserById(user.Id);
        }

        public void DeleteUserById(int id)
        {
            _usersRepository.DeleteById(id);
        }

        public List<UserViewModel> GetAllUsers()
        {
            var users = _usersRepository.GetAll();
            return _mapper.Map<List<UserViewModel>>(users);
        }

        public UserViewModel GetUserById(int id)
        {
            var user = _usersRepository.GetById(id);
            return _mapper.Map<UserViewModel>(user);
        }

        public void UpdateUser(UserViewModel userViewModel)
        {
            var user = _usersRepository.GetById(userViewModel.Id);
            user.RoleKey = userViewModel.RoleKey;
            _usersRepository.Update(user);
        }

        public UserViewModel ValidateUser(UserCredentialsViewModel userCredentialsViewModel)
        {
            var user = _usersRepository.GetByEmail(userCredentialsViewModel.Email);
            if (user == null)
            {
                return null;
            }
            else
            {
                var passwordVerificationResult = PasswordHashedHelper.VerifyHashedPassword(user, userCredentialsViewModel.Password);

                if (passwordVerificationResult == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed)
                {
                    return new UserViewModel();
                }
                else
                {
                    return _mapper.Map<UserViewModel>(user);
                }
            }
        }

        public PagedResultViewModel<UserViewModel> GetFilteredUsers(UsersDatatableRequestViewModel usersDatatableRequestViewModel)
        {
            try
            {
                //LogInfo("Nekoja test poraka so info sto nesto kje ni znaci koga kje go procitame.");
                //throw new NullReferenceException();

                var searchValue = usersDatatableRequestViewModel.search?.value ?? string.Empty;
                var usersPagedResult = _usersRepository.GetFilteredUsers(usersDatatableRequestViewModel.RoleKey,
                                                                         usersDatatableRequestViewModel.start,
                                                                         usersDatatableRequestViewModel.length,
                                                                         searchValue,
                                                                         usersDatatableRequestViewModel.sortColumn,
                                                                         usersDatatableRequestViewModel.isAscending);

                return _mapper.Map<PagedResultViewModel<UserViewModel>>(usersPagedResult);
            }
            catch (Exception ex)
            {
                LogError(ex.Message, ex);
                return new PagedResultViewModel<UserViewModel>();
            }
        }

        public bool HasOtherAdmins(UserViewModel userViewModel)
        {
            var user = _mapper.Map<User>(userViewModel);
            return _usersRepository.HasOtherAdmins(user);
        }
    }
}
