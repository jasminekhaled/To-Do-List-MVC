namespace ToDoList_DomainModel.Models
{
    public class Mission
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int LevelId { get; set; }
        public Level Level { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DueDate { get; set; }
        public bool Complete { get; set; }
    }
}
