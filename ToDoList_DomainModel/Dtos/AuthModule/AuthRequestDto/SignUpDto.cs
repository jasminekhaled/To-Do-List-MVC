using System.ComponentModel.DataAnnotations;

namespace ToDoList_DomainModel.Dtos.AuthModule.AuthRequestDto
{
    public class SignUpDto
    {
        [MaxLength(length: 25)]
        [MinLength(length: 10)]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(length: 30)]
        [MinLength(length: 10)]
        public string Password { get; set; }
    }
}
