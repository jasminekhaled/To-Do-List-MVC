using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ToDoList.AuthMiddleWare;
using ToDoList_DomainModel.Dtos.MissionModule.Response;
using ToDoList_DomainModel.ViewModels.Missions;
using ToDoList_Services.IServices;

namespace ToDoList.Controllers
{
    [CustomAuthorize]
    public class MissionsController : Controller
    {
        private readonly IMissionServices _missionServices;
        public MissionsController(IMissionServices missionServices)
        {
            _missionServices = missionServices;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = int.Parse(HttpContext.Items["userId"] as string);

                var missions = await _missionServices.ListOfMissions(userId);
                var missionViewModel = new ListOfMissionsViewModel()
                {
                    MissionDto = missions,
                    CategoryDtos = await _missionServices.ListOfCategories(),
                };
                return View(missionViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public async Task<IActionResult> CreateMission()
        {
            var viewModel = new CreateMissionViewModel()
            {
                CategoryDtos = await _missionServices.ListOfCategories(),
                LevelDtos = await _missionServices.ListOfLevels()
            };
            return View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> CreateMission(CreateMissionViewModel model)
        {
            try
            {
                var userId = int.Parse(HttpContext.Items["userId"] as string);
                await _missionServices.AddMission(userId, model.AddMissionRequestDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var viewModel = new CreateMissionViewModel()
                {
                    ErrorMessage = ex.Message,
                    CategoryDtos = await _missionServices.ListOfCategories(),
                    LevelDtos = await _missionServices.ListOfLevels(),
                    AddMissionRequestDto = model.AddMissionRequestDto
                };
                return View(viewModel);
            }
        }

        public async Task<IActionResult> EditMission(int id)
        {
            try
            {
                var viewModel = new CreateMissionViewModel()
                {
                    CategoryDtos = await _missionServices.ListOfCategories(),
                    LevelDtos = await _missionServices.ListOfLevels(),
                    AddMissionRequestDto = await _missionServices.GetMissionById(id),
                    // MissionId = id
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditMission(int id, CreateMissionViewModel model)
        {
            try
            {
                var userId = int.Parse(HttpContext.Items["userId"] as string);
                await _missionServices.EditMission(userId, id, model.AddMissionRequestDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var viewModel = new CreateMissionViewModel()
                {
                    CategoryDtos = await _missionServices.ListOfCategories(),
                    LevelDtos = await _missionServices.ListOfLevels(),
                    AddMissionRequestDto = model.AddMissionRequestDto,
                    ErrorMessage = ex.Message
                };
                return View(viewModel);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMission(int id)
        {
            try
            {
                var userId = int.Parse(HttpContext.Items["userId"] as string);
                await _missionServices.DeleteMission(userId, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public async Task<IActionResult> CompleteMission(int id)
        {
            try
            {
                var userId = int.Parse(HttpContext.Items["userId"] as string);
                await _missionServices.CompleteMission(userId, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        [HttpGet]
        public async Task<IActionResult> FilterByCategory(int id)
        {
            try
            {
                var userId = int.Parse(HttpContext.Items["userId"] as string);
                var missions = await _missionServices.FilterByCategory(userId, id);
                var viewModel = new ListOfMissionsViewModel()
                {
                    MissionDto = missions,
                    CategoryDtos = await _missionServices.ListOfCategories()
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
