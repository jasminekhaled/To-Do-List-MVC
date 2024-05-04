using System.ComponentModel.DataAnnotations;

namespace ToDoList_DomainModel.Dtos.AuthModule.AuthRequestDto
{
    public class ResetPasswordDto
    {
        public string OldPassword { get; set; }
        [MaxLength(length: 30)]
        [MinLength(length: 10)]
        public string NewPassword { get; set; }
        [MaxLength(length: 30)]
        [MinLength(length: 10)]
        public string ConfirmNewPassword { get; set; }
    }
}
