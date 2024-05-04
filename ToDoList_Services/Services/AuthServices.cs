using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using ToDoList_DomainModel.Dtos;
using ToDoList_DomainModel.Dtos.AuthModule.AuthRequestDto;
using ToDoList_DomainModel.Dtos.AuthModule.AuthResponseDto;
using ToDoList_DomainModel.Helpers;
using ToDoList_DomainModel.IRepositories;
using ToDoList_DomainModel.Models;
using ToDoList_Services.IServices;

namespace ToDoList_Services.Services
{
    public class AuthServices : IAuthServices
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthServices(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task SignUp(SignUpDto dto)
        {
            try
            {
                var userWithUserName = await _unitOfWork.UserRepository.SingleOrDefaultAsync(u => u.UserName == dto.UserName);
                if(userWithUserName != null )
                 {
                    throw new Exception("This UserName is already used!!");
                }

                var usersEmail = await _unitOfWork.UserRepository.SingleOrDefaultAsync(u => u.Email == dto.Email);
                if (usersEmail != null)
                {
                    throw new Exception("This Email is already used!!");
                }

                var verificationCode = MailService.RandomString(6);
                if (!await MailService.SendEmailAsync(dto.Email, "Verification Code", verificationCode))
                {

                    throw new Exception("Sending the Verification Code is Failed");
                }
                var role = await _unitOfWork.RoleRepository.SingleOrDefaultAsync(s => s.Name == Role.User);

                var user = new User()
                {
                    UserName = dto.UserName,
                    Email = dto.Email,
                    Password = HashingService.GetHash(dto.Password),
                    IsConfirmed = false,
                    VerificationCode = verificationCode,
                    Role = role
                };
                await _unitOfWork.UserRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> VerifyEmail(string email, string verificationCode)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync
                    (s => s.Email == email && s.IsConfirmed == false);
                if(user == null)
                {
                    throw new Exception("SomeThing Went wrong!!");
                }
                if(user.VerificationCode != verificationCode)
                {
                    _unitOfWork.UserRepository.Remove(user);
                    await _unitOfWork.CompleteAsync();

                    throw new Exception("Wrong Verification Code!!");
                }
                user.IsConfirmed = true;
                user.VerificationCode = null;

                _unitOfWork.UserRepository.Update(user);
                var userToken = new UserTokenDto()
                {
                    UserId = user.Id,
                    userName = user.UserName,
                    Role = Role.User
                };
                var Token = TokenService.CreateJwtToken(userToken);
                var refreshToken = TokenService.CreateRefreshToken();
                var newRefreshToken = new RefreshToken
                {
                    Token = refreshToken.Token,
                    ExpiresOn = refreshToken.ExpiresOn,
                    CreatedOn = refreshToken.CreatedOn,
                    userId = user.Id
                };
                await _unitOfWork.RefreshTokenRepository.AddAsync(newRefreshToken);
                await _unitOfWork.CompleteAsync();

                var token = new JwtSecurityTokenHandler().WriteToken(Token);
                return token;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LogInResponseDto> LogIn(LogInDto dto)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(
                    s => s.UserName == dto.UserName && s.IsConfirmed == true);
                if(user == null)
                {
                    throw new Exception("No user found with this user name.");
                }
                if(user.Password != HashingService.GetHash(dto.Password))
                {
                    throw new Exception("Wrong Password.");
                }

                var userToken = new UserTokenDto()
                {
                    UserId = user.Id,
                    userName = user.UserName,
                    Role = Role.User
                };
                var Token = TokenService.CreateJwtToken(userToken);

                var refreshToken = await _unitOfWork.RefreshTokenRepository.GetFirstItem(w => w.userId == user.Id && w.ExpiresOn > DateTime.Now);
                if (refreshToken == null)
                {
                    var token = TokenService.CreateRefreshToken();

                    var newRefreshToken = new RefreshToken
                    {
                        Token = token.Token,
                        ExpiresOn = token.ExpiresOn,
                        CreatedOn = token.CreatedOn,
                        userId = user.Id
                    };
                    await _unitOfWork.RefreshTokenRepository.AddAsync(newRefreshToken);
                    await _unitOfWork.CompleteAsync();
                }

                var tokenn = new JwtSecurityTokenHandler().WriteToken(Token);
                var data = new LogInResponseDto()
                {
                    Token = tokenn,
                    UserId = user.Id
                };
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GeneralResponse<string>> ResetPassword(ResetPasswordDto dto)
        {
            try
            {
                HttpContext httpContext = _httpContextAccessor.HttpContext;
                int userId = httpContext.FindFirst();
                var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(a => a.Id == userId);

                if (user == null || user.Password != HashingService.GetHash(dto.OldPassword))
                {
                    return new GeneralResponse<string>()
                    {
                        IsSuccess = false,
                        Message = "Wrong old Password."
                    };
                }
                if (dto.NewPassword != dto.ConfirmNewPassword)
                {
                    return new GeneralResponse<string>()
                    {
                        IsSuccess = false,
                        Message = "Your Confirmation Password is different than your NewPassword."
                    };
                }
                var HashNewPassword = HashingService.GetHash(dto.NewPassword);
                user.Password = HashNewPassword;
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.CompleteAsync();

                return new GeneralResponse<string>()
                {
                    IsSuccess = true,
                    Message = "Password changed Successfully."
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<string>()
                {
                    IsSuccess = false,
                    Message = "Something Went Wrong.",
                    Error = ex
                };
            }

        }
        public async Task ForgetPassword(ForgetPasswordDto dto)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(s => s.UserName == dto.UserName);
                if (user == null || user.Email != dto.Email)
                {
                    throw new Exception("Wrong Email or UserName.");
                }

                var verificationCode = MailService.RandomString(6);
                if (!await MailService.SendEmailAsync(dto.Email, "Verification Code", verificationCode))
                {
                    throw new Exception("Sending the Verification Code is Failed.");
                }
                user.VerificationCode = verificationCode;
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        
        public async Task ChangeForgettedPassword(string userName, VerifyForgetPassword dto)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(s => s.UserName == userName);
                if (user == null)
                {
                    throw new Exception("no user found, you canot change password");
                }

                if (user.VerificationCode != dto.VerificationCode)
                {
                    throw new Exception("wrong verification code");
                }

                if (dto.NewPassword != dto.ConfirmNewPassword)
                {
                    throw new Exception("Your Confirmation Password is different than your NewPassword.");
                }
                var HashedPassword = HashingService.GetHash(dto.NewPassword);
                user.Password = HashedPassword;
                user.VerificationCode = null;
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.CompleteAsync();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
