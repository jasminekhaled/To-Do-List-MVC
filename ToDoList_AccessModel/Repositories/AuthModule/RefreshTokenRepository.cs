using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList_AccessModel.Context;
using ToDoList_DomainModel.IRepositories.AuthModule;
using ToDoList_DomainModel.Models;

namespace ToDoList_AccessModel.Repositories.AuthModule
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {

        public RefreshTokenRepository(ApplicationDbContext context) : base(context)
        {
        }


    }
}
