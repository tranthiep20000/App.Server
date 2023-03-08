using App.Application.Services.Commom;
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
    public class UserService : BaseService<User, int>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="id">Id</param>
        /// <returns>int</returns>
        /// CreatedBy: ThiepTT(07/03/2023)
        protected override async Task<OperationResult<int>> Validate(User user, int id)
        {
            var result = new OperationResult<int>();

            // 1. username is null
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.USERBYNAMENOTEMPTY);

                return result;
            }

            // get userByUserName
            var userByUserName = await _userRepository.GetByUserName(user.UserName);

            if (id == 0)
            {
                // 2. username is already exist
                if (userByUserName.Data is not null)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.USERBYNAMEALREADYEXIST);

                    return result;
                }
            } 
            else
            {
                // 2. username is already exist
                if (userByUserName.Data is not null)
                {
                    // 2.1 username is already exist
                    if (userByUserName.Data.Email.ToLower().Trim().Equals(user.UserName.ToLower().Trim())
                        && userByUserName.Data.UserId != id)
                    {
                        result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.USERBYNAMEALREADYEXIST);

                        return result;
                    }
                }
            }

            // 3. check length username
            if (user.UserName.Length < ConfigErrorMessageService.LENGTHMINCHARACTEROFUSERNAME
                || user.UserName.Length > ConfigErrorMessageService.LENGTHMAXCHARACTEROFUSERNAME)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.USERBYCHARACTER);

                return result;
            }

            // 4. password is null
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.USERBYPASSWORDNOTEMPTY);

                return result;
            }

            // 5. check password
            if (!IsPasswordValid(user.Password))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.USERBYPASSWORD);

                return result;
            }

            // 6. email is null
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.USERBYEMAILNOTEMPTY);

                return result;
            }

            // get userByEmail
            var userByEmail = await _userRepository.GetByEmail(user.Email);

            if (id == 0)
            {
                // 7. email is already exist
                if (userByEmail.Data is not null)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.USERBYEMAILALREADYEXIST);

                    return result;
                }
            }
            else
            {
                // 7. email is already exist
                if (userByEmail.Data is not null)
                {
                    // 7.1 Email is already exist
                    if (userByEmail.Data.Email.ToLower().Trim().Equals(user.Email.ToLower().Trim())
                        && userByEmail.Data.UserId != id)
                    {
                        result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.USERBYEMAILALREADYEXIST);

                        return result;
                    }
                }
            }

            // 8. check email
            if (!IsEmailValid(user.Email))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.USERBYEMAILFORMAT);

                return result;
            }

            // 9. check length email
            if (user.Email.Length < ConfigErrorMessageService.LENGTHMINCHARACTEROFEMAIL
                || user.Email.Length > ConfigErrorMessageService.LENGTHMAXCHARACTEROFEMAIL)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.USERBYEMAILCHARACTER);

                return result;
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
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.USERBYEMAILFORMAT);

                return result;
            }

            // 2. check length email
            if (user.Email.Length < ConfigErrorMessageService.LENGTHMINCHARACTEROFEMAIL
                || user.Email.Length > ConfigErrorMessageService.LENGTHMAXCHARACTEROFEMAIL)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.USERBYEMAILCHARACTER);

                return result;
            }

            // 3. password is null
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.USERBYPASSWORDNOTEMPTY);

                return result;
            }

            // 4. check length pasword
            if (user.Password.Length < ConfigErrorMessageService.LENGTHMINCHARACTEROFPASSWORD
                || user.Password.Length > ConfigErrorMessageService.LENGTHMAXCHARACTEROFPASSWORD)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.USERBYPASSWORDCHARACTER);

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