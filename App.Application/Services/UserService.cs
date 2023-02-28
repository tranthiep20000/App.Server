﻿using App.Application.Services.Commom;
using App.Domain.Entities;
using App.Domain.Entities.Results;
using App.Domain.Interfaces.IRepositories;
using App.Domain.Interfaces.IServices;
using System.Text.RegularExpressions;

namespace App.Application.Services
{
    /// <summary>
    /// Information of UserService
    /// CreatedBy: ThiepTT(27/02/2022)
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Create a record
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Number record create success</returns>
        /// CreatedBy: ThiepTT(27/02/2022)
        public async Task<OperationResult<int>> Create(User user)
        {
            var result = new OperationResult<int>();

            // 1. username is null
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByNameNotEmpty);

                return result;
            }

            // get userByUserName
            var userByUserName = await _userRepository.GetByUserName(user.UserName);

            // 2. username is already exist
            if (userByUserName.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByNameAlreadyExist);

                return result;
            }

            // 3. check length username
            if (user.UserName.Length < ConfigErrorMessageService.LengthMinCharacterOfUserName
                || user.UserName.Length > ConfigErrorMessageService.LengthMaxCharacterOfUserName)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByCharacter);

                return result;
            }

            // 4. password is null
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByPasswordNotEmpty);

                return result;
            }

            // 5. check password
            if (!IsPasswordValid(user.Password))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByPassword);

                return result;
            }

            // 6. email is null
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailNotEmpty);

                return result;
            }

            // get userByEmail
            var userByEmail = await _userRepository.GetByEmail(user.Email);

            // 7. email is already exist
            if (userByEmail.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailAlreadyExist);

                return result;
            }

            // 8. check email
            if (!IsEmailValid(user.Email))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailFormat);

                return result;
            }

            // 9. check length email
            if (user.Email.Length < ConfigErrorMessageService.LengthMinCharacterOfEmail 
                || user.Email.Length > ConfigErrorMessageService.LengthMaxCharacterOfEmail)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailCharacter);

                return result;
            }

            try
            {
                result = await _userRepository.Create(user);
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: ThiepTT(27/02/2022)
        public async Task<OperationResult<int>> Delete(int id)
        {
            var result = new OperationResult<int>();

            try
            {
                result = await _userRepository.Delete(id);
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="id">Id</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: ThiepTT(27/02/2022)
        public async Task<OperationResult<int>> Update(User user, int id)
        {
            var result = new OperationResult<int>();

            // 1. username is null
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByNameNotEmpty);

                return result;
            }

            // get userByUserName
            var userByUserName = await _userRepository.GetByUserName(user.UserName);

            // 2. username is already exist
            if (userByUserName.Data is not null)
            {
                // 2.1 username is already exist
                if (userByUserName.Data.Email.ToLower().Trim().Equals(user.UserName.ToLower().Trim())
                    && userByUserName.Data.UserId != id)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByNameAlreadyExist);

                    return result;
                }
            }

            // 3. check length username
            if (user.UserName.Length < ConfigErrorMessageService.LengthMinCharacterOfUserName
                || user.UserName.Length > ConfigErrorMessageService.LengthMaxCharacterOfUserName)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByCharacter);

                return result;
            }

            // 4. password is null
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByPasswordNotEmpty);

                return result;
            }

            // 5. check password
            if (!IsPasswordValid(user.Password))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByPassword);

                return result;
            }

            // 6. email is null
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailNotEmpty);

                return result;
            }

            // get userByEmail
            var userByEmail = await _userRepository.GetByEmail(user.Email);

            // 7. email is already exist
            if (userByEmail.Data is not null)
            {
                // 7.1 Email is already exist
                if (userByEmail.Data.Email.ToLower().Trim().Equals(user.Email.ToLower().Trim())
                    && userByEmail.Data.UserId != id)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailAlreadyExist);

                    return result;
                }
            }

            // 8. check email
            if (!IsEmailValid(user.Email))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailFormat);

                return result;
            }

            // 9. check length email
            if (user.UserName.Length < ConfigErrorMessageService.LengthMinCharacterOfEmail
                || user.UserName.Length > ConfigErrorMessageService.LengthMaxCharacterOfEmail)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailCharacter);

                return result;
            }

            try
            {
                result = await _userRepository.Update(user, id);
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Token</returns>
        /// CreatedBy: ThiepTT(28/02/2023)
        public async Task<OperationResult<string>> Login(User user)
        {
            var result = new OperationResult<string>();

            // 1. check email
            if (!IsEmailValid(user.Email))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailFormat);

                return result;
            }

            // 2. check length email
            if (user.Email.Length < ConfigErrorMessageService.LengthMinCharacterOfEmail
                || user.Email.Length > ConfigErrorMessageService.LengthMaxCharacterOfEmail)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByEmailCharacter);

                return result;
            }

            // 3. password is null
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByPasswordNotEmpty);

                return result;
            }

            // 4. check length pasword
            if (user.Password.Length < ConfigErrorMessageService.LengthMinCharacterOfPassword
                || user.Password.Length > ConfigErrorMessageService.LengthMaxCharacterOfPassword)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.UserByPasswordCharacter);

                return result;
            }

            try
            {
                result = await _userRepository.GetUserByEmailPassword(user.Email, user.Password);
            }
            catch (Exception ex)
            {
                result.AddError(ErrorCode.ServerError, $"{ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// IsPasswordValid
        /// </summary>
        /// <param name="password">Password</param>
        /// <returns>bool</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        private static bool IsPasswordValid(string password)
        {
            // Regular expression for password validation
            string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+\\[\]{}|;:',./<>?]).{8,20}$";

            // Validate password against the pattern
            Match match = Regex.Match(password, pattern);

            // Return true if password is valid
            return match.Success;
        }

        /// <summary>
        /// IsEmailValid
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>bool</returns>
        /// CreatedBy: ThiepTT(27/02/2023)
        private static bool IsEmailValid(string email)
        {
            // Regular expression for email validation
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            // Validate password against the pattern
            Match match = Regex.Match(email, pattern);

            // Return true if password is valid
            return match.Success;
        }
    }
}