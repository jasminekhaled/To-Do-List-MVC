using System.ComponentModel.DataAnnotations;

namespace ToDoList_DomainModel.Dtos.AuthModule.AuthRequestDto
{
    public class LogInDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
