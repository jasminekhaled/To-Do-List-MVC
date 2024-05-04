using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList_DomainModel.Dtos.MissionModule.Response;

namespace ToDoList_DomainModel.ViewModels.Missions
{
    public class ListOfMissionsViewModel
    {
        public List<MissionDto> MissionDto { get; set; }
        public int userId { get; set; }
        public List<CategoryDto> CategoryDtos { get; set; }
    }
}
