using System.ComponentModel.DataAnnotations;

namespace ToDoList_DomainModel.Dtos.AuthModule.AuthRequestDto   
{
    public class ForgetPasswordDto
    {
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
