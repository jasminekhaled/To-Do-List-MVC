using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList_DomainModel.Dtos.AuthModule.AuthRequestDto;

namespace ToDoList_DomainModel.ViewModels.Auth
{
    public class ChangePasswordViewModel
    {
        public VerifyForgetPassword VerifyForgetPassword { get; set; }
        public string UserName { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
    }
}
