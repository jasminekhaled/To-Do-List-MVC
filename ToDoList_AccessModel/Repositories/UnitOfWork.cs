using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList_AccessModel.Context;
using ToDoList_AccessModel.Repositories.AuthModule;
using ToDoList_DomainModel.IRepositories.AuthModule;
using ToDoList_DomainModel.IRepositories;
using ToDoList_AccessModel.Repositories.TaskModule;
using ToDoList_DomainModel.IRepositories.TaskModule;

namespace ToDoList_AccessModel.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IUserRepository UserRepository { get; set; }

        public IRoleRepository RoleRepository { get; set; }
        public IMissionRepository MissionRepository { get; set; }
        public ILevelRepository LevelRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            RoleRepository = new RoleRepository(context);
            UserRepository = new UserRepository(context);
            MissionRepository = new MissionRepository(context);
            LevelRepository = new LevelRepository(context);
            RefreshTokenRepository = new RefreshTokenRepository(context);
            CategoryRepository = new CategoryRepository(context);
        }


        public Task<int> CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
