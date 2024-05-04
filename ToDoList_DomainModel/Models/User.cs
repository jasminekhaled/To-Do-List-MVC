namespace ToDoList_DomainModel.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? VerificationCode { get; set; }
        public bool IsConfirmed { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<Mission> Missions { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }

    }
}
