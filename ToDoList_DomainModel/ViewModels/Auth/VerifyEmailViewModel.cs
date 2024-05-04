using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_DomainModel.ViewModels.Auth
{
    public class VerifyEmailViewModel
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
