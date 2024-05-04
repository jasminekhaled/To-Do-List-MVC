using ToDoList_DomainModel.Dtos;
using ToDoList_DomainModel.Dtos.AuthModule.AuthRequestDto;
using ToDoList_DomainModel.Dtos.AuthModule.AuthResponseDto;

namespace ToDoList_Services.IServices
{
    public interface IAuthServices
    {
        Task SignUp(SignUpDto dto);
        Task<LogInResponseDto> LogIn(LogInDto dto);
        Task<string> VerifyEmail(string email, string verificationCode);
        Task<GeneralResponse<string>> ResetPassword(ResetPasswordDto dto);
        Task ForgetPassword(ForgetPasswordDto dto);
        Task ChangeForgettedPassword(string userName, VerifyForgetPassword dto);
    }
}
