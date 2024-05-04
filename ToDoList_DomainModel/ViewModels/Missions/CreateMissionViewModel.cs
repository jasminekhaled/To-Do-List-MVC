using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList_DomainModel.Dtos.MissionModule.Request;
using ToDoList_DomainModel.Dtos.MissionModule.Response;

namespace ToDoList_DomainModel.ViewModels.Missions
{
    public class CreateMissionViewModel
    {
        public int MissionId { get; set; }
        public List<CategoryDto> CategoryDtos { get; set; }
        public List<LevelDto> LevelDtos { get; set; }
        public AddMissionRequestDto AddMissionRequestDto { get; set; }
        public string ErrorMessage { get; set; }

    }
}
