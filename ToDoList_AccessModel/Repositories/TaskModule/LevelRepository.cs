﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList_AccessModel.Context;
using ToDoList_DomainModel.IRepositories.AuthModule;
using ToDoList_DomainModel.IRepositories.TaskModule;
using ToDoList_DomainModel.Models;

namespace ToDoList_AccessModel.Repositories.TaskModule
{
    public class LevelRepository : BaseRepository<Level>, ILevelRepository
    {

        public LevelRepository(ApplicationDbContext context) : base(context)
        {
        }


    }

}
