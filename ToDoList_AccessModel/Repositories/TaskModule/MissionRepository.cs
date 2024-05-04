using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList_AccessModel.Context;
using ToDoList_DomainModel.IRepositories.TaskModule;
using ToDoList_DomainModel.Models;
using Mission = ToDoList_DomainModel.Models.Mission;

namespace ToDoList_AccessModel.Repositories.TaskModule
{
    public class MissionRepository : BaseRepository<Mission>, IMissionRepository
    {

        public MissionRepository(ApplicationDbContext context) : base(context)
        {
        }


    }
}
