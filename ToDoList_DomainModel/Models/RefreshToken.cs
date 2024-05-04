namespace ToDoList_DomainModel.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsActive => DateTime.Now <= ExpiresOn;
        public DateTime CreatedOn { get; set; }
        public int userId { get; set; }
        public User User { get; set; }
    }
}
