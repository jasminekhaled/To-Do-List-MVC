using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList_DomainModel.Dtos;
using ToDoList_DomainModel.Dtos.MissionModule.Request;
using ToDoList_DomainModel.Dtos.MissionModule.Response;
using ToDoList_DomainModel.Helpers;
using ToDoList_DomainModel.IRepositories;
using ToDoList_DomainModel.Models;
using ToDoList_Services.IServices;

namespace ToDoList_Services.Services
{
    public class MissionServices : IMissionServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MissionServices(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<MissionDto>> ListOfMissions(int userId)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(a => a.Id == userId);
                if (user == null)
                    throw new Exception("NO User Found!");

                var missions = await _unitOfWork.MissionRepository.WhereIncludeAsync(w => w.UserId == userId,s => s.Level, s => s.Category);
                var data = _mapper.Map<List<MissionDto>>(missions);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddMission(int userId, AddMissionRequestDto dto)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(a => a.Id == userId);
                if (user == null)
                    throw new Exception("NO User Found!");

                if(!await _unitOfWork.CategoryRepository.AnyAsync(a => a.Id == dto.CategoryId))
                    throw new Exception("NO Category Found!");

                if (!await _unitOfWork.LevelRepository.AnyAsync(a => a.Id == dto.LevelId))
                    throw new Exception("NO Level Found!");

                if(dto.DueDate <= DateTime.Now)
                    throw new Exception("Invalid Date!");

                var mission = _mapper.Map<Mission>(dto);
                mission.Level = await _unitOfWork.LevelRepository.GetByIdAsync(dto.LevelId);
                mission.Category = await _unitOfWork.CategoryRepository.GetByIdAsync(dto.CategoryId);
                mission.Complete = false;
                mission.DueDate = dto.DueDate.ToShortDateString();
                mission.User = await _unitOfWork.UserRepository.GetByIdAsync(userId);
                await _unitOfWork.MissionRepository.AddAsync(mission);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CategoryDto>> ListOfCategories()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            var data = _mapper.Map<List<CategoryDto>>(categories);
            return data;
        }

        public async Task<List<LevelDto>> ListOfLevels()
        {
            var levels = await _unitOfWork.LevelRepository.GetAllAsync();
            var data = _mapper.Map<List<LevelDto>>(levels);
            return data;
        }

        public async Task<AddMissionRequestDto> GetMissionById(int id)
        {
            try
            {
                var mission = await _unitOfWork.MissionRepository.GetByIdAsync(id);
                if (mission == null)
                    throw new Exception("No Mission Found!!");

                var data = _mapper.Map<AddMissionRequestDto>(mission);
                return data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task EditMission(int userId, int missionId, AddMissionRequestDto dto)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
           
                var mission = await _unitOfWork.MissionRepository.GetByIdAsync(missionId);
                if (mission == null)
                    throw new Exception("No Mission Found!!");

                if (user == null || mission.UserId != userId)
                    throw new Exception("You arenot allowed to edit this Task");

                if (dto.DueDate != null && dto.DueDate <= DateTime.Now)
                    throw new Exception("Invalid Date!");

                mission.Title = dto.Title;
                mission.Description = dto.Description;
                mission.Level = await _unitOfWork.LevelRepository.GetByIdAsync(dto.LevelId);
                mission.Category = await _unitOfWork.CategoryRepository.GetByIdAsync(dto.CategoryId);
                mission.DueDate = dto.DueDate.ToShortDateString();
                _unitOfWork.MissionRepository.Update(mission);
                await _unitOfWork.CompleteAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteMission(int userId, int missionId)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

                var mission = await _unitOfWork.MissionRepository.GetByIdAsync(missionId);
                if (mission == null)
                    throw new Exception("No Mission Found!!");

                if (user == null || mission.UserId != userId)
                    throw new Exception("You arenot allowed to delete this Task");

                _unitOfWork.MissionRepository.Remove(mission);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task CompleteMission(int userId, int missionId)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

                var mission = await _unitOfWork.MissionRepository.GetByIdAsync(missionId);
                if (mission == null)
                    throw new Exception("No Mission Found!!");

                if (user == null || mission.UserId != userId)
                    throw new Exception("You arenot allowed to edit this Task");

                if(mission.Complete)
                {
                    mission.Complete = false;
                }
                else
                {
                    mission.Complete = true;
                }
                
                _unitOfWork.MissionRepository.Update(mission);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<MissionDto>> FilterByCategory(int userId, int categoryId)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
                if (user == null)
                    throw new Exception("You arenot allowed");

                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                    throw new Exception("No category Found!!");

                var missions = await _unitOfWork.MissionRepository.WhereIncludeAsync(w => w.CategoryId == categoryId, s => s.Level, s => s.Category); ;
                var data = _mapper.Map<List<MissionDto>>(missions);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
