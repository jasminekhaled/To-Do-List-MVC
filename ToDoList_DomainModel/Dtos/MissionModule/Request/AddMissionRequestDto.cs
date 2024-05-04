using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_DomainModel.Dtos.MissionModule.Request
{
    public class AddMissionRequestDto
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int LevelId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
    }
}
