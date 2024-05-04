namespace ToDoList_DomainModel.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Mission> Missions { get; set; }
    }
}
