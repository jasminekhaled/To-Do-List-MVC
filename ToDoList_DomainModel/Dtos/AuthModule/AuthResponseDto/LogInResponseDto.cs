using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_DomainModel.Dtos.AuthModule.AuthResponseDto
{
    public class LogInResponseDto
    {
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
