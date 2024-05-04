using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList_DomainModel.Dtos.MissionModule.Request;
using ToDoList_DomainModel.Dtos.MissionModule.Response;

namespace ToDoList_Services.IServices
{
    public interface IMissionServices
    {
        Task<List<MissionDto>> ListOfMissions(int userId);
        Task AddMission(int userId, AddMissionRequestDto dto);
        Task<List<CategoryDto>> ListOfCategories();
        Task<List<LevelDto>> ListOfLevels();
        Task<AddMissionRequestDto> GetMissionById(int id);
        Task EditMission(int userId, int missionId, AddMissionRequestDto dto);
        Task DeleteMission(int userId, int missionId);
        Task CompleteMission(int userId, int missionId);
        Task<List<MissionDto>> FilterByCategory(int userId, int categoryId);
    }
}
